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

        public Workflow CreateWorkflow()
        {
            return null;
        }
    }
}
