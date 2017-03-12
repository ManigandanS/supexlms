using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Companies
{
    public interface ICompanyService
    {
        void AddTrialCompany(string firstName, string lastName, string phoneNumber, string companyName, string subDomain, string email, string password);
        Company GetCompanyByHostName(string hostName);
        IEnumerable<Company> LoadAllCompanies();
        IEnumerable<Company> LoadActiveCompanies();
        IEnumerable<Company> LoadTrialCompanies();
        IEnumerable<Company> LoadRegularCompanies();
        

    }
}
