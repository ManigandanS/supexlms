using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Courses
{
    public interface ICourseService
    {
        IEnumerable<Course> LoadAllCourses(string companyId);

        IEnumerable<Course> LoadAllCourses(string companyId, UserTypeEnum userType);

        IEnumerable<Course> FindCourses(string companyId, Func<Course, bool> predicate);

        void DeleteCourse(string companyId, string updaterId, string courseId);
        
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
