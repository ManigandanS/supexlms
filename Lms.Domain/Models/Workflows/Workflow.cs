using Lms.Domain.Models.Companies;
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

    }

    public class Workflow
    {
        public Workflow()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkflowSteps = new HashSet<WorkflowStep>();
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; }
    }
}
