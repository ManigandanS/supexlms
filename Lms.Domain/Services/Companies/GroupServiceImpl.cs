using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Companies
{
    public class GroupServiceImpl : IGroupService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected IUnitOfWork unitOfWork;

        public GroupServiceImpl()
        {

        }

        public GroupServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public Group GetGroupById(string companyId, string groupId)
        {
            var group = unitOfWork.GroupRepository.GetById(groupId);

            if (group.CompanyId == companyId)
                return group;
            else
                return null;
        }

        public Group CreateGroup(string companyId, string name, string description)
        {
            if (unitOfWork.GroupRepository.GetAll().Any(x => x.CompanyId == companyId && x.Name == name.Trim() && !x.IsDeleted))
            {
                throw new CompanyException("Already registered group name.");
            }

            var group = new Lms.Domain.Models.Companies.Group(name, description, true, companyId);
            unitOfWork.GroupRepository.Insert(group);
            unitOfWork.SaveChanges();

            return group;
        }

        public void EditGroup(string companyId, string groupId, string name, string description)
        {
            if (unitOfWork.GroupRepository.GetAll().Any(x => x.CompanyId == companyId && x.Name == name.Trim() && !x.IsDeleted))
            {
                throw new CompanyException("Already registered group name.");
            }

            var group = unitOfWork.GroupRepository.GetById(groupId);
            if (group.CompanyId == companyId)
            {
                group.EditGroup(name, description);

                unitOfWork.GroupRepository.Update(group);
                unitOfWork.SaveChanges();
            }
        }

        public void DeleteGroup(string companyId, string updaterId, string groupId)
        {
            logger.Info("[user: [0}] deletes [group: {1}]", updaterId, groupId);

            var group = unitOfWork.GroupRepository.GetById(groupId);

            logger.Debug("validation: ", group.CompanyId.Equals(companyId));
            if (group.CompanyId.Equals(companyId))
            {
                group.Delete();
                unitOfWork.GroupRepository.Update(group);
                unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Group> LoadAllGroups(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).Groups.Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name);
        }



    }
}
