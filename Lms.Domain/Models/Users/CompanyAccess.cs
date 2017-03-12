using Lms.Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Users
{
    public class CompanyAccess
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public string CompanyId { get; set; }

        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}
