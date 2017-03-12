using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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

        
        

        public void UnpublishCourse(string companyId, string updaterId, string courseId)
        {
            var course = unitOfWork.CourseRepository.GetById(courseId);

            if (course.CompanyId == companyId)
            {
                course.Unpublish();
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
        }

        public void PublishCourse(string companyId, string updaterId, string courseId)
        {
            var course = unitOfWork.CourseRepository.GetById(courseId);

            if (course.CompanyId == companyId)
            {
                course.Publish();
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.SaveChanges();
            }
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

        public IEnumerable<Course> LoadAllCourses(string companyId)
        {
            return unitOfWork.CourseRepository.GetAll().Where(x => x.CompanyId == companyId && !x.IsDeleted).ToList();
        }

        public IEnumerable<Course> LoadAllPublishedCourses(string companyId, string userId)
        {
            var courses = unitOfWork.CourseRepository.FindAsNoTracking(x => x.CompanyId == companyId && !x.IsDeleted && x.IsPublished);
            var user = unitOfWork.UserRepository.GetById(userId);

            if (user.UserType == Models.Users.UserTypeEnum.External)
            {
                courses = courses.Where(x => x.CourseAccess == CourseAccessEnum.ExtenralUsersOnly || x.CourseAccess == CourseAccessEnum.BothUsers);
            }
            else // internal
            {
                courses = courses.Where(x => x.CourseAccess == CourseAccessEnum.InternalUsersOnly || x.CourseAccess == CourseAccessEnum.BothUsers);
            }

            return courses;
        }
    }
}
