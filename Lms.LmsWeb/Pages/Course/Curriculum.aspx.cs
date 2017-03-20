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
    public partial class Curriculum : SecurePage
    {
        protected string enrollmentId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            enrollmentId = Request.QueryString["enid"];
            
            if (!IsPostBack)
            {
                var enrollment = EnrolService.GetEnrollment(enrollmentId);

                if (enrollment != null)
                {
                    logger.Debug("lesson data count: " + enrollment.ScormData.Count);

                    CourseName.Text = enrollment.Session.Course.Name;
                    CourseDesc.Text = enrollment.Session.Course.Description;

                    SessionName.Text = enrollment.Session.Name;
                    SessionDesc.Text = enrollment.Session.Description;

                    EnrollStatus.Text = enrollment.EnrollStatus.ToString();
                    EnrollResult.Text = enrollment.Result.ToString();

                    if (enrollment.IsActiveEnrollment())
                    {
                        WithdrawBtn.Visible = true;
                    }

                    LessonRepeater.DataSource = enrollment.ScormData.OrderBy(x => x.Lesson.Order).ToList();
                    LessonRepeater.DataBind();
                }
            }
        }

        protected void WithdrawBtn_Click(object sender, EventArgs e)
        {
            SessionService.WithdrawEnrollment(enrollmentId, SessionVariable.Current.User.Id);

            Response.Redirect(Request.RawUrl);
        }
    }
}