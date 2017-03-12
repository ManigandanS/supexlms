using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Users;
using Lms.Domain.Models.SSO;
using Ninject;
using Lms.Domain.Repositories;
using Lms.Domain.Services.Companies;
using Lms.Domain.Services.Configs;
using Newtonsoft.Json.Linq;
using Lms.LmsWeb.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Lms.Domain.Models.Utils;

namespace Lms.LmsWeb
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        [Inject]
        public IConfigService ConfigService { get; set; }

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var identityProvider = identity.FindFirst(ClaimTypes.System);
            string email = identity.FindFirst(ClaimTypes.Name).Value;

            Context.GetOwinContext().Authentication.SignOut();

            if (identityProvider != null)
            {
                if (identityProvider.Value == AcquisitionEnum.Office365.ToString())
                {
                    LogoutRequest logoutReq = new LogoutRequest();
                    JObject jsonObj = ConfigService.GetCompanyConfigJsonByHostName(Request.Url.Host, Configuration.OFFICE365_CODE);
                    logoutReq.Issuer = jsonObj["appID"].ToString();
                    logoutReq.NameID = email;
                    string endPoint = jsonObj["endPoint"].ToString();


                    StringWriter sw = new StringWriter();
                    XmlTextWriter tw = null;
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(logoutReq.GetType());
                        tw = new XmlTextWriter(sw);
                        serializer.Serialize(tw, logoutReq);
                    }
                    catch (Exception ex)
                    {
                        
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

                if (identityProvider.Value == AcquisitionEnum.Adfs.ToString())
                {

                }
            }
        }
    }
}