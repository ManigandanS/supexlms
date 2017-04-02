using Lms.Domain.Models.Workflows;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Pages.Catalogue
{
    public partial class Request : SecurePage
    {
        string sessionId, courseId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            sessionId = Request.QueryString["ssid"];
            courseId = Request.QueryString["csid"];

            if (!IsPostBack)
            {
                var session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);
                if (session != null)
                {   
                    CourseName.Text = session.Course.Name;
                    SessionName.Text = session.Name;
                    SessionDate.Text = session.SessionStart + " ~ " + session.SessionEnd;
                    Cost.Text = "$ " + session.Cost.ToString();
                }

                if (SessionVariable.Current.User.UserManagers != null)
                {
                    ApproverName.Text = string.Join(", ", SessionVariable.Current.User.UserManagers.Select(x => x.Manager.DecryptedFullName));
                }
                else
                {
                    Message.Text = "You don't have any manager. To take course, manager approval is needed.";
                    RequestBtn.Visible = false;
                }
            }
        }

        protected void RequestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                WorkflowService.RequestApproval(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, Comment.Text, WorkflowTypeEnum.ExternalCourseTake, sessionId);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        //raise a new exception inserting the current one as the InnerException
                        logger.Error(message);
                    }
                }
                throw raise;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw ex;
            }

            Response.Redirect("Details?csid=" + courseId);
        }
    }
}