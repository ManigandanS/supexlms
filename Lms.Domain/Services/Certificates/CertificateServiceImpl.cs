using Lms.Domain.Models.Certificates;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Certificates
{
    public class CertificateServiceImpl : ICertificateService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected readonly IUnitOfWork unitOfWork;

        private CertificateServiceImpl()
        {

        }

        public CertificateServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        

        public IEnumerable<Certificate> LoadAllCertificates(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).Certificates.Where(x => x.IsDeleted == false);
        }

        public void EditCertificate(string companyId, string userId, string certificateId, string name, string description, CertificateExpiryType expiryType, int? expiryMonth)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var certificate = company.Certificates.SingleOrDefault(x => x.Id == certificateId);

            if (certificate != null)
            {
                certificate.EditCertificate(name, description, expiryType, expiryMonth);
                unitOfWork.CompanyRepository.Update(company);
                unitOfWork.SaveChanges();
            }
        }

        public Certificate CreateCertificate(string companyId, string userId, string name, string description, CertificateExpiryType expiryType, int? expiryMonth)
        {
            var certificate = new Certificate(companyId, name, description, expiryType, expiryMonth);
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            company.AddCertificate(certificate);

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();

            return certificate;
        }

        public void DeleteCertificate(string companyId, string userId, string certificateId)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var certificate = company.Certificates.SingleOrDefault(x => x.Id == certificateId);

            if (certificate != null)
            {
                certificate.Courses.ToList().ForEach(x => x.CertificateId = null);
                certificate.IsDeleted = true;
                unitOfWork.CompanyRepository.Update(company);
                unitOfWork.SaveChanges();
            }

        }

        public Certificate GetCertificateById(string certificateId, string companyId)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var certificate = company.Certificates.SingleOrDefault(x => x.Id == certificateId);

            return certificate;
        }
    }
}
