using Lms.Domain.Models.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Workflows
{
    public class SessionWorkflow : Workflow
    {
        public SessionWorkflow()
        {

        }

        public SessionWorkflow(string companyId, string requestorId, string subject, string comment, WorkflowTypeEnum type, string sessionId)
            : base(companyId, requestorId, subject, comment, type)
        {
            this.SessionId = sessionId;
        }

        [StringLength(128)]
        public string SessionId { get; set; }

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }
    }
}
