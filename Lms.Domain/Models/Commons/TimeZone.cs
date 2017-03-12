using Lms.Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Commons
{
    public class TimeZone
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public int Difference { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
