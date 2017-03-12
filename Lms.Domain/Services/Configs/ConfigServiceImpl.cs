using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Companies;
using Lms.Domain.Repositories;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Configs
{
    public class ConfigServiceImpl : IConfigService
    {
        protected IUnitOfWork unitOfWork;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public ConfigServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void RemoveCompanyConfig(string companyId, string configCode)
        {
            var companyConfig = GetCompanyConfigurationById(companyId, configCode);
            Configuration config = null;

            if (companyConfig != null)
            {
                config = companyConfig.Configuration;
                companyConfig.Configuration.CompanyConfigurations.Remove(companyConfig);
                unitOfWork.ConfigurationRepository.Update(config);
                unitOfWork.SaveChanges();
            }
        }

        public void AddCompanyConfig(string companyId, string configCode, string json)
        {
            var config = unitOfWork.ConfigurationRepository.Find(x => x.Code == configCode).First();
            config.CompanyConfigurations.Add(new CompanyConfiguration() { CompanyId = companyId, ConfigJson = json, ConfigurationId = config.Id });
            unitOfWork.ConfigurationRepository.Update(config);
            unitOfWork.SaveChanges();
        }

        public JObject GetCompanyConfigJsonById(string companyId, string configCode)
        {
            var config = unitOfWork.ConfigurationRepository.Find(x => x.Code == configCode).First();
            var companyConfig = config.CompanyConfigurations.Where(x => x.CompanyId == companyId).FirstOrDefault();

            if (companyConfig != null)
            {
                return JObject.Parse(companyConfig.ConfigJson);
            }
            else
                return null;
        }

        public JObject GetCompanyConfigJsonByHostName(string hostName, string configCode)
        {
            var config = unitOfWork.ConfigurationRepository.Find(x => x.Code == configCode).First();
            var companyConfig = config.CompanyConfigurations.Where(x => x.Company.HostName == hostName).FirstOrDefault();

            if (companyConfig != null)
            {
                return JObject.Parse(companyConfig.ConfigJson);
            }
            else
                return null;
        }

        public CompanyConfiguration GetCompanyConfigurationById(string companyId, string configCode)
        {
            var config = unitOfWork.ConfigurationRepository.Find(x => x.Code == configCode).First();
            return config.CompanyConfigurations.Where(x => x.CompanyId == companyId).FirstOrDefault();
        }

        public CompanyConfiguration GetCompanyConfigurationByHostName(string hostName, string configCode)
        {
            var config = unitOfWork.ConfigurationRepository.Find(x => x.Code == configCode).First();
            return config.CompanyConfigurations.Where(x => x.Company.HostName == hostName).FirstOrDefault();
        }
    }
}
