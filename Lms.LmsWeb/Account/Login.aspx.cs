using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Users;
using Lms.Domain.Models.Utils;
using Lms.Domain.Repositories;
using Lms.Domain.Services.Auths;
using Lms.Domain.Services.Configs;
using Lms.Domain.Services.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
    public partial class Login : System.Web.UI.Page
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IConfigService ConfigService { get; set; }

        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var companyConfig = ConfigService.GetCompanyConfigurationByHostName(Request.Url.Host, Configuration.OFFICE365_CODE);
                if (companyConfig != null)
                    Office365SignIn.Visible = true;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    IAuthenticate auth = new DbAuthenticate(this.UnitOfWork, Request.Url.Host, UserName.Text, Password.Text);
                    var user = AuthService.SignIn(auth);
                    //var user = AuthService.SignIn(Request.Url.Host, UserName.Text, Password.Text);
                    logger.Info("username: {0} result object: {1}", UserName.Text, user);

                    if (user != null)
                    {
                        logger.Debug("start claim settings...{0} - {1}", user.Id, user.Email);
                        var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        identity.AddClaim(new Claim(ClaimTypes.Role, string.Join("\t", user.UserGroups.Select(x => x.GroupId))));
                        identity.AddClaim(new Claim(ClaimTypes.Uri, Request.Url.Host));
                        identity.AddClaim(new Claim(ClaimTypes.System, AcquisitionEnum.OnPremise.ToString()));

                        Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                        logger.Debug("finish claim settings...");

                        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                            Request.GetOwinContext().Response.Redirect(Request.QueryString["ReturnUrl"]);
                        else
                            Request.GetOwinContext().Response.Redirect("/Default");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    throw ex;
                }

                //if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                //    Response.Redirect(Request.QueryString["ReturnUrl"]);
                //else
                //    Response.Redirect("~/Default");
            }
        }
    }
}