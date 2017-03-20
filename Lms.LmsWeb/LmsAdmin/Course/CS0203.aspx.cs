using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Exceptions;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0203 : AdminPage
    {
        protected string sessionId;
        protected Lms.Domain.Models.Courses.Session session;

        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            sessionId = Request.QueryString["ssid"];

            if (!string.IsNullOrEmpty(sessionId))
            {
                session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);

            }

            if (!IsPostBack)
            {
                if (session != null)
                {
                    CourseName.Text = session.Course.Name;

                    switch (session.Course.CourseLocation)
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

                    BindSessionData(session);
                }
            }
        }

        private void BindSessionData(Lms.Domain.Models.Courses.Session session)
        {
            SessionName.Text = session.Name;
            Description.Text = session.Description;
            Cost.Text = session.Cost.ToString();
            SessionDate.Text = session.SessionStart.ToString(CultureInfo.InvariantCulture) + " - " + session.SessionEnd.ToString(CultureInfo.InvariantCulture);
            EnrollDate.Text = session.EnrollStart.ToString(CultureInfo.InvariantCulture) + " - " + session.EnrollEnd.ToString(CultureInfo.InvariantCulture);

            logger.Debug(SessionDate.Text);
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Info(SessionDate.Text + " --- " + EnrollDate.Text);
                var sessionStart = SessionDate.Text.Split(new char[] { '-' })[0].Trim();
                var sessionEnd = SessionDate.Text.Split(new char[] { '-' })[1].Trim();

                var enrollStart = EnrollDate.Text.Split(new char[] { '-' })[0].Trim();
                var enrollEnd = EnrollDate.Text.Split(new char[] { '-' })[1].Trim();

                SessionService.EditSession(SessionVariable.Current.Company.Id, sessionId, SessionName.Text, Description.Text, Cost.Text,
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

            Response.Redirect("CS0201?csid=" + session.CourseId);
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            SessionService.DeleteSession(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, sessionId);
            Response.Redirect("CS0201?csid=" + session.CourseId);
        }
    }
}