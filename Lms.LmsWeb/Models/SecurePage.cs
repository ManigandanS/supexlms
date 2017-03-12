using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Lms.LmsWeb.Models
{
    public class SecurePage : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            AuthenticateUser();
            ((SiteMaster)Page.Master).IsCompanyAdmin = base.IsCompanyAdmin();
        }
    }
}