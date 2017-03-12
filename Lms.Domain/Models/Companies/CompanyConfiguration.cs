using Lms.Domain.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Companies
{
    public class CompanyConfiguration
    {
        [Key, Column(Order = 0)]        
        public string CompanyId { get; set; }
        [Key, Column(Order = 1)]      
        public string ConfigurationId { get; set; }
        public string ConfigJson { get; set; }

        public virtual Company Company { get; set; }
        public virtual Configuration Configuration { get; set; }
    }
}
