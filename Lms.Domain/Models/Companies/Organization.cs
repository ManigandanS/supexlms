using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Companies
{
    public class Organization
    {
        public Organization()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(128)]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }      
        public bool IsDeleted { get; set; }
        public string ParentId { get; set; }

        public virtual Company Company { get; set; }
    }
}
