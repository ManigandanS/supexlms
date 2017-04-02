using Lms.Domain.Gateways.Payments;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Lms.Domain.Services.Courses
{
    public class SessionServiceImpl : ISessionService
    {
        protected IUnitOfWork unitOfWork;
        protected IPaymentService paymentService;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public SessionServiceImpl(IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.paymentService = paymentService;
        }

        

        


        public void WithdrawEnrollment(string enrollmentId, string userId)
        {
            var enroll = unitOfWork.EnrollmentRepository.GetById(enrollmentId);

            if (enroll.UserId == userId)
            {
                enroll.Withdraw();
                unitOfWork.EnrollmentRepository.Update(enroll);
                unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Session> LoadNewSessions(string companyId, UserTypeEnum userType)
        {
            var courses = unitOfWork.CourseRepository.GetAll().Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .Include(x => x.Sessions);

            if (userType == Models.Users.UserTypeEnum.External)
            {
                courses = courses.Where(x => x.CourseAccess == CourseAccessEnum.ExtenralUsersOnly || x.CourseAccess == CourseAccessEnum.BothUsers);
            }
            else // internal
            {
                courses = courses.Where(x => x.CourseAccess == CourseAccessEnum.InternalUsersOnly || x.CourseAccess == CourseAccessEnum.BothUsers);
            }

            var utcNow = DateTime.UtcNow;
            return courses.SelectMany(x => x.Sessions).Where(x => !x.IsDeleted && x.EnrollEnd >= utcNow).OrderByDescending(x => x.SessionStart).Take(6).ToList();
        }



        public Session GetSessionById(string companyId, string sessionId)
        {
            var session = unitOfWork.CourseRepository.GetAll().SelectMany(x => x.Sessions).SingleOrDefault(x => x.Id == sessionId);

            if (session != null && session.Course.CompanyId == companyId)
            {
                return session;
            }

            return null;

        }



        public void CreateSession(string companyId, string courseId, string sessionName, string description, string cost,
            string sessionStartDate, string sessionEndDate, string enrollmentStartDate, string enrollmentEndDate)
        {
            Course course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                var sessionStart = DateTime.Parse(sessionStartDate);
                var sessionEnd = DateTime.Parse(sessionEndDate);
                var enrollmentStart = DateTime.Parse(enrollmentStartDate);
                var enrollmentEnd = DateTime.Parse(enrollmentEndDate);
                double? doubleCost = null;
                if (!string.IsNullOrEmpty(cost))
                    doubleCost = double.Parse(cost);

                course.AddSession(sessionName, description, doubleCost, sessionStart, sessionEnd, enrollmentStart, enrollmentEnd);
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
        }

        public void EditSession(string companyId, string sessionId, string sessionName, string description, string cost,
            string sessionStartDate, string sessionEndDate, string enrollmentStartDate, string enrollmentEndDate)
        {
            Session session = unitOfWork.SessionRepository.GetById(sessionId);
            if (session.Course.CompanyId == companyId)
            {
                var sessionStart = DateTime.Parse(sessionStartDate);
                var sessionEnd = DateTime.Parse(sessionEndDate);
                var enrollmentStart = DateTime.Parse(enrollmentStartDate);
                var enrollmentEnd = DateTime.Parse(enrollmentEndDate);
                double? doubleCost = null;
                if (!string.IsNullOrEmpty(cost))
                    doubleCost = double.Parse(cost);

                session.Name = sessionName;
                session.Description = description;
                session.Cost = doubleCost;
                session.SessionStart = sessionStart;
                session.SessionEnd = sessionEnd;
                session.EnrollStart = enrollmentStart;
                session.EnrollEnd = enrollmentEnd;

                unitOfWork.SessionRepository.Update(session);
                unitOfWork.SaveChanges();
            }
        }

        public void DeleteSession(string companyId, string updaterId, string sessionId)
        {
            logger.Info("[user: {0}] deletes a session [sessionId: {1}]", updaterId, sessionId);
            var session = unitOfWork.SessionRepository.GetById(sessionId);
            if (session.Course.Company.Id == companyId)
            {
                if (session != null)
                {
                    session.IsDeleted = true;
                    session.UpdatedTs = DateTime.UtcNow;

                    unitOfWork.SessionRepository.Update(session);
                    unitOfWork.SaveChanges();
                }
            }
        }



    }
}
