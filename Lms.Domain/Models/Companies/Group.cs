using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Companies
{
    public class Group
    {
        
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Group(string name, string description, bool canDeleted, string companyId) : this()
        {
            this.Name = name;
            this.Description = description;
            this.CanDeleted = canDeleted;
            this.CompanyId = companyId;
        }

        public Group(string name, bool canDeleted, string companyId) : this()
        {
            this.Name = name;
            this.CanDeleted = canDeleted;
            this.CompanyId = companyId;
        }

        public void EditGroup(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public void Delete()
        {
            if (CanDeleted)
            {
                this.IsDeleted = true;
                UserGroups.Clear();

                /*
                foreach (var userGroup in UserGroups)
                {
                    UserGroups.Remove(userGroup);
                }
                */
            }
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }        

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        
        [StringLength(8)]
        public string ShortCode { get; set; }

        public bool CanDeleted { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
