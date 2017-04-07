using Lms.Domain.Gateways.Payments;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Enrolments
{
    public class EnrolServiceImpl : IEnrolService
    {
        protected IUnitOfWork unitOfWork;
        protected IPaymentService paymentService;
        static Logger logger = LogManager.GetCurrentClassLogger();

        private EnrolServiceImpl()
        {

        }

        public EnrolServiceImpl(IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.paymentService = paymentService;
        }

        public void Test()
        {
            logger.Info("TEST RESULT................");
        }

        public void EnrollUser(string userId, string sessionId)
        {
            //var lessons = unitOfWork.SessionRepository.GetById(sessionId).Course.Lessons.Where(x => !x.IsDeleted).ToList();
            var enrollment = new Enrollment(userId, sessionId);
            unitOfWork.EnrollmentRepository.Insert(enrollment);
            unitOfWork.SaveChanges();
        }

        public void ChargeSession(string userId, string sessionId, string cardNumber, string expireYear, string expireMonth, string cvv2)
        {
            try
            {
                logger.Info("userId: {0}, sessionId: {1}, cvv2: {2}", userId, sessionId, cvv2);

                var session = unitOfWork.SessionRepository.GetById(sessionId);
                if (session == null)
                    logger.Error("request session is null");

                if (session != null && session.Cost != null && session.Cost > 0)
                {

                    paymentService.Charge((int)(session.Cost.Value * 100), "usd", "", cardNumber, expireYear, expireMonth, cvv2);

                    EnrollUser(userId, sessionId);
                }
            }
            catch (PaymentException pex)
            {
                logger.Error(pex.ToString());
                throw pex;
            }
        }

        public IEnumerable<Enrollment> LoadFinishedCourses(string userId)
        {
            var user = unitOfWork.UserRepository.GetById(userId);
            return user.Enrollments.Where(x => x.Result != EnrollResultEnum.Pending).Union(user.Enrollments.Where(x => x.Session.SessionEnd < DateTime.UtcNow));
        }

        public Enrollment GetEnrollment(string enrollmentId)
        {
            return unitOfWork.EnrollmentRepository.GetById(enrollmentId);
        }

        public IEnumerable<Enrollment> LoadActiveEnrollments(string userId)
        {
            List<Enrollment> activeEnrollments = new List<Enrollment>();
            var enrollments = unitOfWork.UserRepository.GetById(userId).Enrollments;
            logger.Debug("total enroll count: " + enrollments.Count);

            foreach (var item in enrollments)
            {
                if (item.IsActiveEnrollment())
                {
                    activeEnrollments.Add(item);
                }
            }

            return activeEnrollments;
        }
    }
}
