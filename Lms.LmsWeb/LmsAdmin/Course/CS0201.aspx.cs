using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0201 : AdminPage
    {
        protected string courseId;

        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request.QueryString["csid"];

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    var course = CourseService.GetCourseById(SessionVariable.Current.Company.Id, courseId);

                    if (course != null)
                    {
                        Repeater1.DataSource = course.Sessions.Where(x => !x.IsDeleted).OrderByDescending(x => x.SessionStart).ToList();
                        Repeater1.DataBind();
                    }
                }
            }
        }

        protected void DelBtn_Command(object sender, CommandEventArgs e)
        {
            string sessionId = e.CommandArgument as string;
            SessionService.DeleteSession(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, sessionId);

            Response.Redirect(Request.RawUrl);
        }
    }
}