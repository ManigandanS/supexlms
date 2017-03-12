using Lms.Domain.Models.SSO;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lms.Domain.Services.Auths
{
    public class AzureAuthenticate : IAuthenticate
    {
        readonly IUnitOfWork unitOfWork;
        readonly string hostName;
        readonly Response azureResponse;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public AzureAuthenticate(IUnitOfWork unitOfWork, string hostName, string azureResponse)
        {
            this.unitOfWork = unitOfWork;
            this.hostName = hostName;
            XmlSerializer serializer = new XmlSerializer(typeof(Response));
            using (TextReader reader = new StringReader(azureResponse))
            {
                this.azureResponse = (Response)serializer.Deserialize(reader);
            }
        }

        public User AuthenticateUser()
        {
            string email = null, firstName = null, lastName = null;
            object temp = null;

            temp = azureResponse.Assertion.AttributeStatement.Where(x => x.Name == ClaimTypes.Name).FirstOrDefault();
            if (temp != null)
                email = ((AssertionAttribute)temp).AttributeValue;

            temp = azureResponse.Assertion.AttributeStatement.Where(x => x.Name == ClaimTypes.GivenName).FirstOrDefault();
            if (temp != null)
                firstName = ((AssertionAttribute)temp).AttributeValue;

            temp = azureResponse.Assertion.AttributeStatement.Where(x => x.Name == ClaimTypes.Surname).FirstOrDefault();
            if (temp != null)
                lastName = ((AssertionAttribute)temp).AttributeValue;

            logger.Debug("login request --- hostName: {0}, email: {1}, firstName: {2}, lastName: {3}", 
                hostName, email, firstName, lastName);

            var company = unitOfWork.CompanyRepository.Find(x => x.HostName == hostName).FirstOrDefault();
            var user = unitOfWork.UserRepository.Find(x => x.Email == email && !x.IsDeleted && x.Acquisition == AcquisitionEnum.Office365).FirstOrDefault();

            if (user == null)
            {
                company.AddUser(email, firstName, lastName, null, UserTypeEnum.Internal, UserStatusEnum.Active, false, AcquisitionEnum.Office365, null, null);
                
            }
            else
            {
                bool hasAccess = company.CompanyAccesses.Any(x => x.UserId == user.Id);
                if (!hasAccess)
                {
                    company.AddCompanyAccess(user);
                }
            }

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();

            return user;
        }
    }
}
