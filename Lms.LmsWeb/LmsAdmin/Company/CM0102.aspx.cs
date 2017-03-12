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
    public partial class CM0102 : AdminPage
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
                    GroupService.CreateGroup(
                        SessionVariable.Current.Company.Id, Name.Text, Description.Text);
                }
                catch (CompanyException cex)
                {
                    logger.Info(cex.ToString());
                }
            }

            Response.Redirect("CM0101");
         
        }
    }
}