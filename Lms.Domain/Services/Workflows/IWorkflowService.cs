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
        Workflow CreateWorkflow();
    }
}
