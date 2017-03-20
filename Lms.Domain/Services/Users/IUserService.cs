using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(string hostName, string username, string password, string firstName, string lastName);

        void RegisterTemporaryUser(string companyId, string updaterId, string email, string password,
            string firstName, string lastName, UserTypeEnum userType, List<string> groups, List<string> roles);

        void AddManager(string companyId, string userId, string managerId);

        void RemoveManager(string companyId, string userId, string managerId);

        IEnumerable<User> LoadAllUsers(string companyId);

        IEnumerable<User> LoadActiveUsers(string companyId);

        IEnumerable<User> LoadInactiveUsers(string companyId);

        IEnumerable<User> LoadRegisteredUsers(string companyId);        

        IEnumerable<Role> LoadActiveRolls(string companyId);

        User GetUserById(string companyId, string userId);

        void DeleteUser(string companyId, string updaterId, string userId);

        void CreateRole(string companyId, string updaterId, string name, string description);

        void DeleteRole(string companyId, string updaterId, string roleId);

        IEnumerable<UserCertificate> LoadUserCertificates(string userId);

        IEnumerable<User> FindManagersByName(string companyId, string userId, string name);
    }
}
