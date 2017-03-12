using Lms.Domain.Models.Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Certificates
{
    public interface ICertificateService
    {
        IEnumerable<Certificate> LoadAllCertificates(string companyId);

        Certificate CreateCertificate(string companyId, string userId, string name, string description, CertificateExpiryType expiryType, int? month);

        void DeleteCertificate(string companyId, string userId, string certificateId);

        Certificate GetCertificateById(string certificateId, string companyId);

        void EditCertificate(string companyId, string userId, string certificateId, string name, string description, CertificateExpiryType expiryType, int? month);

    }
}
