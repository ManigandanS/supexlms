using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
using Lms.Domain.Models.Utils;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Auths
{
    public class AuthServiceImpl : IAuthService
    {
        protected IUnitOfWork unitOfWork;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public AuthServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User SignIn(IAuthenticate authenticate)
        {
            return authenticate.AuthenticateUser();
        }

        public User SignIn(string hostName, string email, string password)
        {
            logger.Debug("login request --- hostName: {0}, email: {1}, password: {2}", hostName, email, password);

            var user = unitOfWork.UserRepository.GetAll().FirstOrDefault(x => x.Email == email && !x.IsDeleted);

            if (user == null)
                throw new UserException("Invalid username or password.");

            logger.Trace("user is not null");

            var companyAccess = user.CompanyAccesses.FirstOrDefault(x => x.Company.HostName == hostName);
            if (companyAccess == null)
                throw new UserException("Invalid username or password.");

            logger.Trace("company access is not null");

            logger.Trace("password: {0}, db password: {1}", CryptoUtil.DecryptFromBase64(user.Password, user.Id), password);
            if (CryptoUtil.DecryptFromBase64(user.Password, user.Id) == password)
                return user;
            else
                throw new UserException("Invalid username or password.");
        }
    }
}
