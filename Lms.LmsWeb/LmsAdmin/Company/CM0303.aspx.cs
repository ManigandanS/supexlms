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

namespace Lms.LmsWeb.LmsAdmin.Company
{
    public partial class CM0303 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        protected string notificationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            notificationId = Request.QueryString["id"];

            if (!IsPostBack)
            {
                var notification = NotificationService.GetNotificationById(SessionVariable.Current.Company.Id, notificationId);
                if (notification != null)
                {
                    TitleText.Text = notification.Title;
                    StartDate.Text = notification.StartDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    EndDate.Text = notification.EndDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    Details.Text = HttpUtility.HtmlDecode(notification.Details);
                }
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    NotificationService.EditNotification(
                        SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                        notificationId, TitleText.Text, Details.Text, StartDate.Text, EndDate.Text);
                }
                catch (CompanyException cex)
                {
                    logger.Info(cex.ToString());
                }
            }

            Response.Redirect("CM0301");

        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            NotificationService.DeleteNotification(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, notificationId);

            Response.Redirect("CM0301");
        }
    }
}