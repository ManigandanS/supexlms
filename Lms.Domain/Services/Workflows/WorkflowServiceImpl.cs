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

        public Workflow RequestApproval(string companyId, string requestorId, string comment, WorkflowTypeEnum type, string sessionId)
        {
            if (type == WorkflowTypeEnum.ExternalCourseTake)
            {
                Workflow workflow = new SessionWorkflow(companyId, requestorId, comment, type, sessionId);
                User user = unitOfWork.UserRepository.GetById(requestorId);
                ICreateWorkflowStep stepGen = new ManagerWorkflowStep(user);
                foreach (var step in stepGen.Create(workflow.Id))
                    workflow.WorkflowSteps.Add(step);
                
                unitOfWork.WorkflowRepository.Insert(workflow);

                var enrollment = new Enrollment(requestorId, sessionId);
                enrollment.EnrollStatus = EnrollStatusEnum.Pending;
                unitOfWork.EnrollmentRepository.Insert(enrollment);

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
    }
}
