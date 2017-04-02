using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Workflows
{
    public interface ICreateWorkflowStep
    {
        List<WorkflowStep> Create(string workflowId);
    }
}
