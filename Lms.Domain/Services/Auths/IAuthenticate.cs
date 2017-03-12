using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Auths
{
    public interface IAuthenticate
    {
        User AuthenticateUser();
    }
}
