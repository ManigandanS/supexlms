using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Courses
{
    public class CourseServiceImpl : ICourseService
    {
        protected IUnitOfWork unitOfWork;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public CourseServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Course GetCourseById(string companyId, string courseId)
        {
            var course = unitOfWork.CourseRepository.GetById(courseId);

            if (course.CompanyId != companyId)
                return null;
            else
                return course;
        }

        

        

        

        public Course CreateCourse(string companyId, string updaterId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess)
        {
            logger.Info("[user: {0}] creates a course [name: {1}, desc: {2}, type: {3}, loc: {4}, access: {5}]",
                updaterId, courseName, courseDescription, courseType, courseLocation, courseAccess);

            Course course = new Course(companyId, courseName, courseDescription, courseType, courseLocation, courseAccess);

            unitOfWork.CourseRepository.Insert(course);
            unitOfWork.SaveChanges();

            return course;
        }

        public Course CreateCourse(string companyId, string updaterId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess, string certificateId)
        {
            logger.Info("[user: {0}] creates a course [name: {1}, desc: {2}, type: {3}, loc: {4}, access: {5}, certificate: {6}]",
                updaterId, courseName, courseDescription, courseType, courseLocation, courseAccess, certificateId);

            Course course = new Course(companyId, courseName, courseDescription, courseType, courseLocation, courseAccess, certificateId);

            unitOfWork.CourseRepository.Insert(course);
            unitOfWork.SaveChanges();

            return course;
        }

        public Course EditCourse(string companyId, string updaterId, string courseId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess)
        {
            Course course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                course.Update(courseName, courseDescription, courseType, courseLocation, courseAccess);
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
                return course;
            }
            else
                return null;
        }

        public Course EditCourse(string companyId, string updaterId, string courseId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess, string certificateId)
        {
            Course course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.CompanyId == companyId)
            {
                course.Update(courseName, courseDescription, courseType, courseLocation, courseAccess, certificateId);
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
                return course;
            }
            else
                return null;
        }

        

        public void DeleteCourse(string companyId, string updaterId, string courseId)
        {
            logger.Info("[user: {0}] deletes a course [courseId: {1}]", updaterId, courseId);

            var course = unitOfWork.CourseRepository.GetById(courseId);
            if (course.Company.Id == companyId)
            {
                course.IsDeleted = true;
                course.UpdatedTs = DateTime.UtcNow;

                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Course> LoadAllCourses(string companyId, UserTypeEnum userType)
        {
            var result = unitOfWork.CourseRepository.GetAllAsNoTracking().Where(x => x.CompanyId == companyId && !x.IsDeleted);

            if (userType == Models.Users.UserTypeEnum.External)
            {
                return result.Where(x => x.CourseAccess == CourseAccessEnum.ExtenralUsersOnly || x.CourseAccess == CourseAccessEnum.BothUsers);
            }
            else // internal
            {
                return result.Where(x => x.CourseAccess == CourseAccessEnum.InternalUsersOnly || x.CourseAccess == CourseAccessEnum.BothUsers);
            }
        }

        public IEnumerable<Course> LoadAllCourses(string companyId)
        {
            return unitOfWork.CourseRepository.GetAllAsNoTracking().Where(x => x.CompanyId == companyId && !x.IsDeleted);
        }

        public IEnumerable<Course> FindCourses(string companyId, Func<Course, bool> predicate)
        {
            return unitOfWork.CourseRepository.FindAsNoTracking(x => x.CompanyId == companyId).Where(predicate).ToList();
        }
    }
}
