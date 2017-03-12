using Lms.Domain.Services.Companies;
using Lms.WebApp.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using Lms.Domain.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Web.Administration;
using System.IO;
namespace Lms.WebApp
{
    public partial class Trial : BasePage
    {
        [Inject]
        public ICompanyService CompanyService { get; set; }
        protected ServerManager serverMgr = null;

        readonly static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CreateAppPool(string poolname, bool enable32bitOn64, ManagedPipelineMode mode, string runtimeVersion = "v4.0")
        {
            using (ServerManager serverManager = new ServerManager())
            {
                ApplicationPool newPool = serverManager.ApplicationPools.Add(poolname);
                newPool.ManagedRuntimeVersion = runtimeVersion;
                newPool.Enable32BitAppOnWin64 = true;
                newPool.ManagedPipelineMode = mode;
                serverManager.CommitChanges();
            }
        }

        private static void CreateIISWebsite(string websiteName, string hostname, string phyPath, string appPool)
        {
            string virtualPath = ConfigurationManager.AppSettings["LmsWebVirDir"] + "\\" + websiteName;
            if (!Directory.Exists(virtualPath))
            {
                Directory.CreateDirectory(virtualPath);
            }

            ServerManager iisManager = new ServerManager();
            iisManager.Sites.Add(websiteName, "http", "*:80:" + hostname, phyPath);
            iisManager.Sites[websiteName].ApplicationDefaults.ApplicationPoolName = appPool;
            iisManager.Sites[websiteName].Applications.First().VirtualDirectories.Add("/data", virtualPath);
            foreach (var item in iisManager.Sites[websiteName].Applications)
            {
                item.ApplicationPoolName = appPool;
            }

            iisManager.CommitChanges();
        }

        protected void RequestButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string lmsWebUrl = SubDomain.Text + "." + ConfigurationManager.AppSettings["LmsWebDomain"];

                try
                {
                    CreateAppPool(lmsWebUrl, true, ManagedPipelineMode.Integrated, "v4.0");  
                    CreateIISWebsite(lmsWebUrl, lmsWebUrl, ConfigurationManager.AppSettings["LmsWebPath"], lmsWebUrl);  

                    CompanyService.AddTrialCompany(FirstName.Text, LastName.Text, PhoneNumber.Text,
                            CompanyName.Text, SubDomain.Text, Email.Text, Password.Text);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    CustomValidator1.IsValid = false;
                    CustomValidator1.ErrorMessage = ex.Message;

                    return;
                }

                Response.Redirect("http://" + lmsWebUrl);
            }
        }
    }
}