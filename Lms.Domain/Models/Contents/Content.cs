using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Contents
{
    public abstract class Content
    {
        [Key]
        [StringLength(128)]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        [StringLength(128)] 
        public string CompanyId { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedTs { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }

        public virtual Company Company { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User User { get; set; }
    }
}
