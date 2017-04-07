using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Pages.Workflow
{
    public partial class Review : SecurePage
    {
        string workflowId;

        protected void Page_Load(object sender, EventArgs e)
        {
            workflowId = Request.QueryString["wkid"];
        }
    }
}