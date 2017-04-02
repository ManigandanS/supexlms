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
    public class WorkflowStep
    {
        private WorkflowStep()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public WorkflowStep(string workflowId, string userId, int step) : this()
        {
            this.WorkflowId = workflowId;
            this.UserId = userId;
            this.Step = step;
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string WorkflowId { get; set; }

        public int Step { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string ApprovedBy { get; set; }
        
        public DateTime? CompleteTs { get; set; }

        public string Comment { get; set; }

        public virtual Workflow Workflow { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("ApprovedBy")]
        public virtual User Approver { get; set; }
    }
}
