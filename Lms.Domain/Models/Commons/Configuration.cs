using Lms.Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Commons
{
    public class Configuration
    {
        public const string OFFICE365_CODE = "ST0002";
        public const string ADFS_CODE = "ST0003";


        public Configuration()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Configuration(string title, string description) : this()
        {
            this.Title = title;
            this.Description = description;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        public virtual ICollection<CompanyConfiguration> CompanyConfigurations { get; set; }
    }
}
