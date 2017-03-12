using Lms.Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Companies
{
    public interface IGroupService
    {
        IEnumerable<Group> LoadAllGroups(string companyId);
        Group CreateGroup(string companyId, string name, string description);
        void DeleteGroup(string companyId, string updaterId, string groupId);
        void EditGroup(string companyId, string groupId, string name, string description);
        Group GetGroupById(string companyId, string groupId);
    }
}
