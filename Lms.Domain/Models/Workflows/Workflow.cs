using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Workflows
{
    public enum WorkflowTypeEnum
    {
        ExternalCourseTake,
        ExternalCourseResult,
        InternalCourseTake,        
    }

    public enum WorkflowStatusEnum
    {
        Requested,
        Withdrawn,
        Completed
    }

    public enum WorkflowProcessStatusEnum
    {
        Pending,
        Approved,
        Declined
    }

    public class Workflow
    {
        public Workflow()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkflowSteps = new HashSet<WorkflowStep>();
        }

        public Workflow(string companyId, string requestorId, string subject, string comment, WorkflowTypeEnum type) : this()
        {
            this.CompanyId = companyId;
            this.WorkflowType = type;
            this.WorkflowStatus = WorkflowStatusEnum.Requested;
            this.WorkflowProcessStatus = WorkflowProcessStatusEnum.Pending;
            this.RequestorId = requestorId;
            this.RequestTs = DateTime.UtcNow;
            this.NextStep = 1;
            this.Comment = comment;
            this.Subject = subject;
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public WorkflowTypeEnum WorkflowType { get; set; }

        public WorkflowStatusEnum WorkflowStatus { get; set; }

        public WorkflowProcessStatusEnum WorkflowProcessStatus { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [StringLength(128)]
        public string RequestorId { get; set; }

        public string Comment { get; set; }

        public int? NextStep { get; set; }

        public DateTime RequestTs { get; set; }

        public DateTime? CompleteTs { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; }
        public virtual User Requestor { get; set; }
    }
}
