using Lms.Domain.Models.Exceptions;
using Lms.Domain.Services.Companies;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Company
{
    public partial class CM0302 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    NotificationService.CreateNotification(
                        SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, TitleText.Text, Details.Text, StartDate.Text, EndDate.Text);
                }
                catch (CompanyException cex)
                {
                    logger.Info(cex.ToString());
                }
            }

            Response.Redirect("CM0301");
         
        }
    }
}