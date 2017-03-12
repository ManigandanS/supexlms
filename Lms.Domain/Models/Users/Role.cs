using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Users
{
    public class Role
    {

        public Role()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Role(string name, string description, string companyId)
            : this()
        {
            this.Name = name;
            this.Description = description;
            this.CompanyId = companyId;
        }

        public Role(string name, string description)
            : this()
        {
            this.Name = name;
            this.Description = description;
        }

        public void Delete()
        {

            this.IsDeleted = true;
            UserRoles.Clear();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
