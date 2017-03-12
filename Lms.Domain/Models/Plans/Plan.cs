using Lms.Domain.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Plans
{
    public class Plan
    {
        public Plan()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(128)]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(128)]
        public string CurrencyId { get; set; }

        public double Price { get; set; }

        public long MaxUser { get; set; }
        public long MaxStorage { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
