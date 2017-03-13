using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Course
{
    public partial class Details : SecurePage
    {
        protected string courseId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request.QueryString["id"];
            
            if (!IsPostBack)
            {
                var course = CourseService.GetCourseById(SessionVariable.Current.Company.Id, courseId);

                if (course != null)
                {
                    CourseName.Text = course.Name;
                    CourseDesc.Text = course.Description;

                    LessonRepeater.DataSource = course.LoadActiveLessons();
                    LessonRepeater.DataBind();

                    SessionRepeater.DataSource = course.LoadActiveSessions();
                    SessionRepeater.DataBind();
                }
            }
        }

        protected void SessionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var session = e.Item.DataItem as Session;
                var enrollButton = e.Item.FindControl("EnrollBtn");

                Literal enrollStatus = (Literal)e.Item.FindControl("EnrollStatus");
                HyperLink enrollLink = (HyperLink)e.Item.FindControl("EnrollLink");

                if (session.IsEnrolledUser(SessionVariable.Current.User.Id))
                {

                    enrollStatus.Text = session.GetEnrollStatus(SessionVariable.Current.User.Id);
                    if (enrollStatus.Text == EnrollStatusEnum.Enrolled.ToString())
                    {
                        enrollLink.CssClass = "btn btn-success btn-sm";
                    }

                    if (enrollStatus.Text == EnrollStatusEnum.Withdrawn.ToString())
                    {
                        enrollLink.CssClass = "btn btn-warning btn-sm";
                    }

                    enrollButton.Visible = false;
                }
                else
                {
                    if (session.SessionEnd < DateTime.UtcNow || session.EnrollEnd < DateTime.UtcNow || session.EnrollStart > DateTime.UtcNow)
                    {
                        enrollStatus.Text = "N/A";
                        enrollLink.CssClass = "btn btn-danger btn-sm";
                        enrollButton.Visible = false;
                    }
                }
            }
        }

        protected void EnrollBtn_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var sessionId = e.CommandArgument as string;

                var session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);

                if (session.Cost != null & session.Cost > 0)
                {
                    Response.Redirect("Payment?ssid=" + sessionId);
                }
                else
                    SessionService.EnrollUser(SessionVariable.Current.User.Id, sessionId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}