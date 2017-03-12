using Lms.Domain.Models.Certificates;
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
    public class UserCertificate
    {
        [Key, Column(Order = 0)]
        [StringLength(128)]
        public string UserId { get; set; }
        [Key, Column(Order = 1)]
        [StringLength(128)]
        public string CertificateId { get; set; }
        public DateTime IssuedTs { get; set; }
        public DateTime? ExpireTs { get; set; }

        public virtual User User { get; set; }
        public virtual Certificate Certificate { get; set; }
    }
}
