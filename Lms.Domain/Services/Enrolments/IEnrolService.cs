using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Enrolments
{
    public interface IEnrolService
    {
        IEnumerable<Enrollment> LoadFinishedCourses(string userId);

        Enrollment GetEnrollment(string enrollmentId);

        IEnumerable<Enrollment> LoadActiveEnrollments(string userId);
    }
}
