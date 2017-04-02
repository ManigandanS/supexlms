using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Workflows
{
    public class ManagerWorkflowStep : ICreateWorkflowStep
    {
        readonly User user;

        public ManagerWorkflowStep(User user)
        {
            this.user = user;
        }

        public List<WorkflowStep> Create(string workflowId)
        {
            List<WorkflowStep> workflowSteps = new List<WorkflowStep>();

            var managers = user.UserManagers.Select(x => x.Manager);
            foreach (var manager in managers)
            {
                WorkflowStep workflowStep = new WorkflowStep(workflowId, manager.Id, 1);
                workflowSteps.Add(workflowStep);
            }

            return workflowSteps;
        }
    }
}
