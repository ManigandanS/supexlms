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

namespace Lms.LmsWeb.LmsAdmin.Account
{
    public partial class AC0102 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                UserService.CreateRole(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                    Name.Text, Description.Text);
            }

            Response.Redirect("AC0101");
         
        }
    }
}