using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Emails;
using Lms.Domain.Models.Plans;
using Lms.Domain.Models.Quizzes;
using Lms.Domain.Models.Contents;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private Hashtable repositories;

        public UnitOfWork()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IDbRepository<Configuration> ConfigurationRepository
        {
            get { return Repository<Configuration>(); }
        }

        public IDbRepository<Session> SessionRepository
        {
            get { return Repository<Session>(); }
        }

        public IDbRepository<Enrollment> EnrollmentRepository
        {
            get { return Repository<Enrollment>(); }
        }

        public IDbRepository<Quiz> QuizRepository
        {
            get { return Repository<Quiz>(); }
        }

        public IDbRepository<Course> CourseRepository 
        {
            get { return Repository<Course>(); }
        }

        public IDbRepository<Plan> PlanRepository
        {
            get { return Repository<Plan>(); }
        }

        public IDbRepository<Group> GroupRepository
        {
            get { return Repository<Group>(); }
        }

        public IDbRepository<EmailQueue> EmailQueueRepository
        {
            get { return Repository<EmailQueue>(); }
        }

        public IDbRepository<Company> CompanyRepository
        {
            get { return Repository<Company>(); }
        }

        public IDbRepository<Scorm> ScormRepository
        {
            get { return Repository<Scorm>(); }
        }

        public IDbRepository<User> UserRepository
        {
            get { return Repository<User>(); }
        }

        private IDbRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(DbRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), dbContext);

                repositories.Add(type, repositoryInstance);
            }

            return (IDbRepository<T>)repositories[type];
        }

        public void SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SqlCommand(string sql, params object[] parameters)
        {
            return dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
