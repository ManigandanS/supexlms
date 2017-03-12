using Lms.Domain.Models.Companies;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Configs
{
    public interface IConfigService
    {
        JObject GetCompanyConfigJsonById(string companyId, string configCode);
        JObject GetCompanyConfigJsonByHostName(string hostName, string configCode);
        CompanyConfiguration GetCompanyConfigurationById(string companyId, string configCode);
        CompanyConfiguration GetCompanyConfigurationByHostName(string hostName, string configCode);
        void RemoveCompanyConfig(string companyId, string configCode);
        void AddCompanyConfig(string companyId, string configCode, string json);
    }
}
