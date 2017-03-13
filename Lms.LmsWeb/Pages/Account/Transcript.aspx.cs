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
    public partial class Transcript : SecurePage
    {

        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            GradeRepeater.DataSource = UserService.LoadFinishedCourses(SessionVariable.Current.User.Id);
            GradeRepeater.DataBind();

            CertificateRepeater.DataSource = UserService.LoadUserCertificates(SessionVariable.Current.User.Id);
            CertificateRepeater.DataBind();
        }
        
    }
}