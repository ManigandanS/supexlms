using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Auths
{
    public interface IAuthService
    {
        User SignIn(string hostName, string email, string password);
        User SignIn(IAuthenticate authenticate);
    }
}
