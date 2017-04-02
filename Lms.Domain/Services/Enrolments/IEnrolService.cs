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
        void EnrollUser(string userId, string sessionId);

        void ChargeSession(string userId, string sessionId, string cardNumber, string expireYear, string expireMonth, string cvv2);

        IEnumerable<Enrollment> LoadFinishedCourses(string userId);

        Enrollment GetEnrollment(string enrollmentId);

        IEnumerable<Enrollment> LoadActiveEnrollments(string userId);
    }
}
