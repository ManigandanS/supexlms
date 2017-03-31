using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Pages.Catalogue
{
    public partial class Request : SecurePage
    {
        string sessionId;

        protected void Page_Load(object sender, EventArgs e)
        {
            sessionId = Request.QueryString["ssid"];

            if (!IsPostBack)
            {
                var session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);
                if (session != null)
                {   
                    CourseName.Text = session.Course.Name;
                    SessionName.Text = session.Name;
                    SessionDate.Text = session.SessionStart + " ~ " + session.SessionEnd;
                    Cost.Text = "$ " + session.Cost.ToString();
                }

                if (SessionVariable.Current.User.UserManagers != null)
                {
                    ApproverName.Text = string.Join(", ", SessionVariable.Current.User.UserManagers.Select(x => x.Manager.DecryptedFullName));
                }
                else
                {
                    Message.Text = "You don't have any manager. To take course, manager approval is needed.";
                    RequestBtn.Visible = false;
                }
            }
        }

        protected void RequestBtn_Click(object sender, EventArgs e)
        {

        }
    }
}