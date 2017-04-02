using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Emails;
using Lms.Domain.Models.Plans;
using Lms.Domain.Models.Quizzes;
using Lms.Domain.Models.Contents;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lms.Domain.Models.Certificates;
using Lms.Domain.Models.Workflows;

namespace Lms.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IDbRepository<Configuration> ConfigurationRepository { get; }
        IDbRepository<Company> CompanyRepository { get; }
        IDbRepository<Scorm> ScormRepository { get; }
        IDbRepository<User> UserRepository { get; }
        IDbRepository<EmailQueue> EmailQueueRepository { get; }
        IDbRepository<Group> GroupRepository { get; }
        IDbRepository<Plan> PlanRepository { get; }
        IDbRepository<Course> CourseRepository { get; }
        IDbRepository<Quiz> QuizRepository { get; }
        IDbRepository<Enrollment> EnrollmentRepository { get; }
        IDbRepository<Session> SessionRepository { get; }
        IDbRepository<Certificate> CertificateRepository { get; }
        IDbRepository<Workflow> WorkflowRepository { get; }

        int SqlCommand(string sql, params object[] parameters);
        void SaveChanges();

        void Dispose(bool disposing);
    }
}
