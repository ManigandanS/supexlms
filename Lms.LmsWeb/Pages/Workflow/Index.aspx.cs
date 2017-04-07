using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Pages.Workflow
{
    public partial class Index : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActiveRepeater.DataSource = WorkflowService.LoadActiveTasks(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id);
                ActiveRepeater.DataBind();

                ClosedRepeater.DataSource = WorkflowService.LoadClosedTasks(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id);
                ClosedRepeater.DataBind();
            }
        }
    }
}