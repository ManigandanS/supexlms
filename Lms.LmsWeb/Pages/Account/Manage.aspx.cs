using Lms.Domain.Models.Utils;
using Lms.Domain.Repositories;
using Lms.Domain.Services.Users;
using Lms.LmsWeb.Models;
using Microsoft.AspNet.Identity;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Account
{
    public partial class Manage : SecurePage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = SessionVariable.Current.User;

            Name.Text = user.DecryptedFullName;
            Email.Text = user.Email;
        }

        
    }
}