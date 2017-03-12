using Lms.Domain.Models.Commons;
using Lms.Domain.Models.SSO;
using Lms.Domain.Models.Users;
using Lms.Domain.Models.Utils;
using Lms.Domain.Repositories;
using Lms.Domain.Services.Auths;
using Lms.Domain.Services.Companies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Lms.LmsWeb.Account
{
    public partial class Azure : System.Web.UI.Page
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public ICompanyService CompanyService { get; set; }

        string username = string.Empty, email = string.Empty, surname = string.Empty, givenname = string.Empty;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            string samlResponse = Request.Form["SAMLResponse"];

            logger.Info(samlResponse);

            if (!string.IsNullOrEmpty(samlResponse))
            {
                string decodedResponse = SamlUtil.DecodeResponse(samlResponse);

                if (Request.IsAuthenticated)
                    Request.GetOwinContext().Authentication.SignOut();


                IAuthenticate auth = new AzureAuthenticate(UnitOfWork, Request.Url.Host, decodedResponse);
                var user = AuthService.SignIn(auth);

                logger.Debug("user: " + user);

                if (user != null)
                {
                    logger.Debug("start claim settings...{0} - {1}", user.Id, user.Email);

                    var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Role, string.Join("\t", user.UserGroups.Select(x => x.GroupId))));
                    identity.AddClaim(new Claim(ClaimTypes.Uri, Request.Url.Host));
                    identity.AddClaim(new Claim(ClaimTypes.System, AcquisitionEnum.Office365.ToString()));

                    Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        Request.GetOwinContext().Response.Redirect(Request.QueryString["ReturnUrl"]);
                    else
                        Request.GetOwinContext().Response.Redirect("/Default");
                }
            }
            else
            {
                logger.Trace("Azure login...");

                AuthnRequest authnReq = new AuthnRequest();
                var config = UnitOfWork.ConfigurationRepository.FindAsNoTracking(x => x.Code == Configuration.OFFICE365_CODE).FirstOrDefault();
                var company = CompanyService.GetCompanyByHostName(Request.Url.Host);

                logger.Trace("config: {0}, company: {1}", config, company);

                if (config == null || company == null)
                    return;


                var companyConfig = company.CompanyConfigurations.First(x => x.ConfigurationId == config.Id);
                var jsonObj = JObject.Parse(companyConfig.ConfigJson);
                var issuer = jsonObj["appID"].ToString();
                var endPoint = jsonObj["endPoint"].ToString();
                authnReq.Issuer = issuer;


                StringWriter sw = new StringWriter();
                XmlTextWriter tw = null;
                try
                {
                    XmlSerializer serializer = new XmlSerializer(authnReq.GetType());
                    tw = new XmlTextWriter(sw);
                    serializer.Serialize(tw, authnReq);
                }
                catch (Exception ex)
                {
                    //Handle Exception Code
                    logger.Error(ex.ToString());
                }
                finally
                {
                    sw.Close();
                    if (tw != null)
                    {
                        tw.Close();
                    }
                }



                Response.Redirect(string.Format("{0}?SAMLRequest={1}", endPoint, SamlUtil.EncodeRequest(sw.ToString())));
            }
        }
    }
}