using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0102 : AdminPage
    {
        protected string courseId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request.QueryString["csid"];

            if (!IsPostBack)
            {
                ScormRepeater.DataSource = ScormService.LoadPublishedScorms(SessionVariable.Current.Company.Id).OrderBy(x => x.Name);
                ScormRepeater.DataBind();

                QuizRepeater.DataSource = QuizService.LoadPublishedQuizzes(SessionVariable.Current.Company.Id).OrderBy(x => x.Title);
                QuizRepeater.DataBind();

                var course = CourseService.GetCourseById(SessionVariable.Current.Company.Id, courseId);
                if (course.CourseType == CourseTypeEnum.External)
                {
                    Panel1.Visible = false;
                }

            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            logger.Trace("Create button clicked...");

            try
            {

                if (Panel1.Visible == false)
                {
                    LessonService.CreateExternalLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId,
                                LessonName.Text, Description.Text);
                }
                else
                {
                    switch (LessonType.SelectedValue)
                    {
                        case "0":
                            LessonService.CreateScormLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId, ScormId.Value,
                                LessonName.Text, Description.Text);
                            break;
                        case "1":
                            LessonService.CreateQuizLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId, QuizId.Value,
                                LessonName.Text, Description.Text);
                            break;
                        case "2":
                            //CourseService.CreateAssignmentLesson();
                            break;
                        default:
                            
                            break;
                    }
                }
            }
            catch (CourseException cex)
            {
                logger.Info(cex.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            Response.Redirect("CS0101?csid=" + courseId);
        }
    }
}