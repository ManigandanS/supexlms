using Lms.Domain.Models.Users;
using Lms.Domain.Models.Workflows;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Workflows
{
    public class WorkflowServiceImpl : IWorkflowService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected readonly IUnitOfWork unitOfWork;

        private WorkflowServiceImpl()
        {

        }

        public WorkflowServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Workflow RequestApproval(string companyId, string requestorId, string subject, string comment, WorkflowTypeEnum type, string sessionId)
        {
            if (type == WorkflowTypeEnum.ExternalCourseTake)
            {
                Workflow workflow = new SessionWorkflow(companyId, requestorId, subject, comment, type, sessionId);
                User user = unitOfWork.UserRepository.GetById(requestorId);
                ICreateWorkflowStep stepGen = new ManagerWorkflowStep(user);
                foreach (var step in stepGen.Create(workflow.Id))
                    workflow.WorkflowSteps.Add(step);
                
                unitOfWork.WorkflowRepository.Insert(workflow);
                unitOfWork.SaveChanges();
                return workflow;
            }

            return null;
        }

        public void ApproveRequest(string companyId, string workflowStepId, string userId, string comment)
        {

        }

        public void DeclineRequest(string companyId, string workflowStepId, string userId, string comment)
        {

        }

        public IEnumerable<Workflow> LoadActiveTasks(string companyId, string userId)
        {
            IEnumerable<Workflow> workflows = unitOfWork.WorkflowRepository.GetAllAsNoTracking()
                .Where(x => x.CompanyId == companyId && x.WorkflowProcessStatus == WorkflowProcessStatusEnum.Pending && x.WorkflowStatus == WorkflowStatusEnum.Requested)
                .SelectMany(x => x.WorkflowSteps)
                .Where(x => x.UserId == userId && x.Step == x.Workflow.NextStep).Select(x => x.Workflow).ToList();

            return workflows;
        }

        public IEnumerable<Workflow> LoadClosedTasks(string companyId, string userId)
        {
            IEnumerable<Workflow> workflows = unitOfWork.WorkflowRepository.GetAllAsNoTracking()
                .Where(x => x.CompanyId == companyId).SelectMany(x => x.WorkflowSteps)
                .Where(x => (x.UserId == userId || x.ApprovedBy == userId) && (x.Step < x.Workflow.NextStep || x.Workflow.NextStep == null)).Select(x => x.Workflow).ToList();

            return workflows;
        }
    }
}
