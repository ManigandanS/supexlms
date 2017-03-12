using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb
{
    public partial class _Default : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActiveNotifications.DataSource = NotificationService.LoadActiveNotifications(SessionVariable.Current.Company.Id)
                    .OrderByDescending(x => x.StartDate).ToList();
                ActiveNotifications.DataBind();

                var activeCourses = UserService.LoadActiveEnrollments(SessionVariable.Current.User.Id).OrderBy(x => x.Session.Course.Name).ToList();
                ActiveCourses.DataSource = activeCourses;
                ActiveCourses.DataBind();

                if (activeCourses.Count == 0)
                    Panel1.Visible = false;


                Panel2.Visible = false;


                var newCourses = SessionService.LoadNewSessions(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id).OrderBy(x => x.Course.Name).ToList();
                NewCourses.DataSource = newCourses;
                NewCourses.DataBind();

                if (newCourses.Count == 0)
                    Panel3.Visible = false;
            }
        }


    }
}