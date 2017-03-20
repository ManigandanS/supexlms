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
        static Logger logger = LogManager.GetCurrentClassLogger();

        public EnrolServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
