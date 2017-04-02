using Lms.Domain.Models.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Workflows
{
    public interface IWorkflowService
    {
        Workflow RequestApproval(string companyId, string requestorId, string comment, WorkflowTypeEnum type, string sessionId);
        void ApproveRequest(string companyId, string workflowStepId, string userId, string comment);
        void DeclineRequest(string companyId, string workflowStepId, string userId, string comment);
    }
}
