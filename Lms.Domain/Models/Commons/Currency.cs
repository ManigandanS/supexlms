using Lms.Domain.Models.Plans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Commons
{
    public class Currency
    {
        public Currency()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Code { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
