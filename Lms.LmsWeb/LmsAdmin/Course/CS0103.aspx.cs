using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Exceptions;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0103 : AdminPage
    {
        protected string courseId, lessonId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request.QueryString["csid"];
            lessonId = Request.QueryString["lsid"];

            if (!IsPostBack)
            {
                ScormRepeater.DataSource = ScormService.LoadAllScorms(SessionVariable.Current.Company.Id).OrderBy(x => x.Name);
                ScormRepeater.DataBind();

                QuizRepeater.DataSource = QuizService.LoadAllQuizzes(SessionVariable.Current.Company.Id).OrderBy(x => x.Title);
                QuizRepeater.DataBind();

                var lesson = LessonService.GetLessonById(SessionVariable.Current.Company.Id, courseId, lessonId);
                if (lesson != null)
                {
                    LessonName.Text = lesson.Name;
                    Description.Text = lesson.Description;

                    if (lesson.Course.CourseType == CourseTypeEnum.External)
                    {
                        Panel1.Visible = false;
                    }
                    else
                    {
                        LessonType.SelectedValue = ((int)lesson.LessonType).ToString();
                        ScormId.Value = lesson.ScormId;
                        QuizId.Value = lesson.QuizId;
                    }
                }


            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Panel1.Visible == false)
                {
                    LessonService.EditExternalLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId, lessonId,
                                LessonName.Text, Description.Text);
                }
                else
                {
                    switch (LessonType.SelectedValue)
                    {
                        case "0":
                            LessonService.EditScormLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId, lessonId, ScormId.Value,
                                LessonName.Text, Description.Text);
                            break;
                        case "1":
                            LessonService.EditQuizLesson(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId, lessonId, QuizId.Value,
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