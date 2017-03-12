using Lms.Domain.Gateways.Payments;
using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
using Lms.Domain.Models.Utils;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Companies
{
    public class CompanyServiceImpl : ICompanyService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected IUnitOfWork unitOfWork;

        public CompanyServiceImpl()
        {

        }

        public CompanyServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        

        public Company GetCompanyByHostName(string hostName)
        {
            return unitOfWork.CompanyRepository.GetAll().Where(x => x.IsDeleted == false && x.HostName == hostName).FirstOrDefault();
        }        

        public void AddTrialCompany(string firstName, string lastName, string phoneNumber, string companyName, string subDomain, string email, string password)
        {
            logger.Info("firstName: {0}, lastName: {1}, companyName: {2}, email: {3}",
                firstName, lastName, companyName, email);

            string hostName = ToUrlSlug(subDomain) + "." + ConfigurationManager.AppSettings["LmsWebDomain"];
            logger.Debug("URL: " + hostName);

            if (unitOfWork.CompanyRepository.GetAll().Any(x => x.HostName == hostName))
            {
                throw new CompanyException(string.Format("The company name {0} is already registered in the system", email));
            }


            try
            {
                var company = new Company(firstName, lastName, phoneNumber, companyName, email, true, DateTime.UtcNow.AddMonths(1), hostName);
                var user = new User(email, firstName, lastName, password, UserTypeEnum.Internal, UserStatusEnum.Active, false, AcquisitionEnum.OnPremise);
                logger.Debug("new company id: " + company.Id);
                var group = new Lms.Domain.Models.Companies.Group("Administrator", "Company Administrator", false, company.Id);

                user.AddUserGroup(group);
                company.AddCompanyAccess(user);

                unitOfWork.CompanyRepository.Insert(company);
                unitOfWork.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbex)
            {
               
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        logger.Error(message);
                    }
                }
                throw dbex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw ex;
            }
        }

        public IEnumerable<Company> LoadAllCompanies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> LoadActiveCompanies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> LoadTrialCompanies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> LoadRegularCompanies()
        {
            throw new NotImplementedException();
        }


        public string ToUrlSlug(string value)
        {

            //First to lower case 
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);

            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces 
            value = System.Text.RegularExpressions.Regex.Replace(value, @"\s", "-", System.Text.RegularExpressions.RegexOptions.Compiled);

            //Remove invalid chars 
            value = System.Text.RegularExpressions.Regex.Replace(value, @"[^\w\s\p{Pd}]", "", System.Text.RegularExpressions.RegexOptions.Compiled);

            //Trim dashes from end 
            value = value.Trim('-', '_');

            //Replace double occurences of - or \_ 
            value = System.Text.RegularExpressions.Regex.Replace(value, @"([-_]){2,}", "$1", System.Text.RegularExpressions.RegexOptions.Compiled);

            return value;
        }
    }
}
