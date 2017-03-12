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
    public partial class CS0101 : AdminPage
    {
        protected Domain.Models.Courses.Course course;

        protected void Page_Load(object sender, EventArgs e)
        {
            string courseId = Request.QueryString["csid"];


            if (!string.IsNullOrEmpty(courseId))
            {
                course = CourseService.GetCourseById(SessionVariable.Current.Company.Id, courseId);

                if (course != null)
                {
                    CreateLink.NavigateUrl = "CS0102?csid=" + courseId;

                    if (course.IsPublished)
                        CreateLink.Visible = false;
                }
            }

            if (!IsPostBack)
            {
                Repeater1.DataSource = course.Lessons.Where(x => !x.IsDeleted).OrderBy(x => x.Order).ToList();
                Repeater1.DataBind();
            }
        }

        protected void DelBtn_Command(object sender, CommandEventArgs e)
        {
            string lessonId = e.CommandArgument as string;
            LessonService.DeleteLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, course.Id, lessonId);

            Response.Redirect(Request.RawUrl);
        }
    }
}