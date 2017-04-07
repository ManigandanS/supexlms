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

namespace Lms.LmsWeb.Catalogue
{
    public partial class Details : SecurePage
    {
        protected string courseId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request.QueryString["csid"];
            
            if (!IsPostBack)
            {
                var course = CourseService.GetCourseById(SessionVariable.Current.Company.Id, courseId);

                if (course != null)
                {
                    CourseName.Text = course.Name;
                    CourseDesc.Text = course.Description;

                    if (course.CourseType == CourseTypeEnum.Intenral)
                    {
                        LessonRepeater.DataSource = course.LoadActiveLessons();
                        LessonRepeater.DataBind();
                    }
                    else
                        Panel1.Visible = false;

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

                Literal enrollText = (Literal)e.Item.FindControl("EnrollText");

                string message = string.Empty;
                bool canEnrol = session.CanEnrolCourse(SessionVariable.Current.User.Id, out message);
                logger.Info("canEnrol: {0}, message: {1}", canEnrol, message);
                if (!canEnrol)
                {
                    enrollText.Text = message;
                    enrollButton.Visible = false;
                }
            }
        }

        protected void EnrollBtn_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var sessionId = e.CommandArgument as string;

                var session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);

                if (session.Course.CourseType == CourseTypeEnum.External)
                    Response.Redirect("Request?ssid=" + sessionId + "&csid=" + courseId);
                

                if (session.Cost != null & session.Cost > 0)
                {
                    Response.Redirect("Payment?ssid=" + sessionId + "&csid=" + courseId);
                }
                else
                    EnrolService.EnrollUser(SessionVariable.Current.User.Id, sessionId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}