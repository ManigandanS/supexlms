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
    public partial class CS0202 : AdminPage
    {
        protected string courseId;
        static Logger logger = LogManager.GetCurrentClassLogger();

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
                        CourseName.Text = course.Name;

                        switch (course.CourseLocation)
                        {
                            case CourseLocationEnum.Offline:
                                OfflineSession.Visible = true;
                                break;
                            case CourseLocationEnum.Online:
                                OnlineSession.Visible = true;
                                break;
                            default:
                                OnlineSession.Visible = true;
                                OfflineSession.Visible = true;
                                break;
                        }
                    }
                }
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Info(SessionDate.Text + " --- " + EnrollDate.Text);
                var sessionStart = SessionDate.Text.Split(new char[] { '-' })[0].Trim();
                var sessionEnd = SessionDate.Text.Split(new char[] { '-' })[1].Trim();

                var enrollStart = EnrollDate.Text.Split(new char[] { '-' })[0].Trim();
                var enrollEnd = EnrollDate.Text.Split(new char[] { '-' })[1].Trim();
                
                SessionService.CreateSession(SessionVariable.Current.Company.Id, courseId, SessionName.Text, Description.Text, Cost.Text,
                    sessionStart, sessionEnd, enrollStart, enrollEnd);
            }
            catch (CourseException cex)
            {
                logger.Info(cex.ToString()); 
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            Response.Redirect("CS0201?csid=" + courseId);
        }
    }
}