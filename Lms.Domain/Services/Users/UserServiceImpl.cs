using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
using Lms.Domain.Models.Utils;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Users
{
    public class UserServiceImpl : IUserService
    {
        protected IUnitOfWork unitOfWork;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public UserServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetUserById(string companyId, string userId)
        {
            var user = unitOfWork.UserRepository.GetById(userId);

            if (user != null && user.CompanyAccesses.Any(x => x.CompanyId == companyId))
                return user;
            else
                return null;
        }

        public IEnumerable<UserCertificate> LoadUserCertificates(string userId)
        {
            return unitOfWork.UserRepository.GetById(userId).UserCertificates.ToList();
        }

        public IEnumerable<Enrollment> LoadFinishedCourses(string userId)
        {
            var user = unitOfWork.UserRepository.GetById(userId);
            return user.Enrollments.Where(x => x.Result != EnrollResultEnum.Pending).Union(user.Enrollments.Where(x => x.Session.SessionEnd < DateTime.UtcNow));
        }

        public void CreateRole(string companyId, string updaterId, string name, string description)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            company.AddRole(name, description);
            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();
        }

        public void DeleteRole(string companyId, string updaterId, string roleId)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var role = company.Roles.Single(x => x.Id == roleId);
            foreach (var userRole in role.UserRoles.ToList())
            {
                role.UserRoles.Remove(userRole);
            }
            role.IsDeleted = true;
            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<Role> LoadActiveRolls(string companyId)
        {
            return unitOfWork.CompanyRepository.GetAll().Single(x => x.Id == companyId).Roles.Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name).ToList();
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

        public void RegisterUser(string hostName, string username, string password, string firstName, string lastName)
        {

        }

        public void RegisterTemporaryUser(string companyId, string updaterId, string email, string password,
            string firstName, string lastName, UserTypeEnum userType, List<string> groups, List<string> roles)
        {
            if (unitOfWork.UserRepository.GetAll()
                .Any(x1 => x1.IsDeleted == false && x1.Email == email && x1.CompanyAccesses.Any(x2 => x2.CompanyId == companyId)))
            {
                throw new UserException("Email " + email + " is already taken.");
            }


            var company = unitOfWork.CompanyRepository.GetById(companyId);
            company.AddUser(email, firstName, lastName, password, userType, UserStatusEnum.Active, true, AcquisitionEnum.OnPremise, groups, roles);

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<User> LoadAllUsers(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).CompanyAccesses
                .Select(x => x.User).Where(x => x.IsDeleted == false);
        }

        public IEnumerable<User> LoadActiveUsers(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).CompanyAccesses
                .Select(x => x.User).Where(x => x.IsDeleted == false && x.Status == UserStatusEnum.Active);
        }

        public IEnumerable<User> LoadInactiveUsers(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).CompanyAccesses
                .Select(x => x.User).Where(x => x.IsDeleted == false && x.Status == UserStatusEnum.Inactive);
        }

        public IEnumerable<User> LoadRegisteredUsers(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).CompanyAccesses
                .Select(x => x.User).Where(x => x.IsDeleted == false && x.Status == UserStatusEnum.Registered);
        }

        private int GetNumberOfAdmin(string companyId)
        {
            return unitOfWork.UserRepository.GetAll().Where(x => x.IsDeleted == false)
                    .SelectMany(x => x.UserGroups).Join(unitOfWork.GroupRepository.GetAll().Where(x => x.Name == "Administrator"),
                    x1 => x1.GroupId, x2 => x2.Id, (x1, x2) => x2).Count();
        }

        public void DeleteUser(string companyId, string updaterId, string userId)
        {
            var user = unitOfWork.UserRepository.GetById(userId);

            if (user.CompanyAccesses.Any(x => x.CompanyId == companyId))
            {
                int adminNum = GetNumberOfAdmin(companyId);

                logger.Info("Current admin num: " + adminNum);

                if (adminNum < 2)
                    throw new UserException("At least one administrator should exist in the company");

                user.IsDeleted = true;
                unitOfWork.UserRepository.Update(user);
                unitOfWork.SaveChanges();
            }
        }


        
    }
}
