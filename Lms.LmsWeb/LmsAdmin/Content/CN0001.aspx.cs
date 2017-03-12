using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Content
{
    public partial class CN0001 : AdminPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScormRepeater.DataSource = ScormService.LoadAllScorms(SessionVariable.Current.Company.Id);
                ScormRepeater.DataBind();
            }
        }


        protected void PublishBtn_Command(object sender, CommandEventArgs e)
        {
            string scormId = e.CommandArgument as string;
            ScormService.PublishScorm(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, scormId);

            Response.Redirect(Request.RawUrl);
        }

        
        

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}