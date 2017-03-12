using Lms.Domain.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Courses
{
    public interface ICourseService
    {
        IEnumerable<Course> LoadAllCourses(string companyId);

        IEnumerable<Course> LoadAllPublishedCourses(string companyId, string userId);

        void DeleteCourse(string companyId, string updaterId, string courseId);

        void PublishCourse(string companyId, string updaterId, string courseId);

        void UnpublishCourse(string companyId, string updaterId, string courseId);

        Course CreateCourse(string companyId, string updaterId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess);

        Course CreateCourse(string companyId, string updaterId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess, string certificateId);

        Course EditCourse(string companyId, string updaterId, string courseId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess);

        Course EditCourse(string companyId, string updaterId, string courseId, string courseName, string courseDescription,
            string courseType, string courseLocation, string courseAccess, string certificateId);

        Course GetCourseById(string companyId, string courseId);
        
    }
}
