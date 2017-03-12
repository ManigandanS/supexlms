using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Certificates
{
    public enum CertificateExpiryType
    {
        NoExpiry,
        Term,
        Fiscal,
    }

    public class Certificate
    {
        public Certificate()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Certificate(string companyId, string name, string descrpition) : this()
        {
            this.CompanyId = companyId;
            this.Name = name;
            this.Description = descrpition;
            this.UpdatedTs = DateTime.UtcNow;
            this.ExpiryType = CertificateExpiryType.NoExpiry;
        }

        public Certificate(string companyId, string name, string descrpition, CertificateExpiryType expiryType, int? expiryMonth)
            : this()
        {
            this.CompanyId = companyId;
            this.Name = name;
            this.Description = descrpition;
            this.UpdatedTs = DateTime.UtcNow;
            this.ExpiryType = expiryType;
            this.ExpiryMonth = expiryMonth;
        }

        public void EditCertificate(string name, string description, CertificateExpiryType expiryType, int? expiryMonth)
        {
            this.Name = name;
            this.Description = description;
            this.ExpiryType = expiryType;
            this.ExpiryMonth = expiryMonth;
        }

        public IEnumerable<Course> GetAttachedCourses()
        {
            return this.Courses.Where(x => !x.IsDeleted);
        }

        [Key]
        [StringLength(128)]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public DateTime UpdatedTs { get; set; }

        public CertificateExpiryType ExpiryType { get; set; }

        public DateTime? FiscalExpiryDate { get; set; }
        public int? ExpiryMonth { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<UserCertificate> UserCertificates { get; set; }
        
    }
}
