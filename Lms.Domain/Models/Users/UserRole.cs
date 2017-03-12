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
    public class UserRole
    {
        [Key, Column(Order = 0)]
        [StringLength(128)]
        public string UserId { get; set; }
        [Key, Column(Order = 1)]
        [StringLength(128)]
        public string RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
