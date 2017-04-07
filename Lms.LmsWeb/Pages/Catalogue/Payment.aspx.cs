using Lms.Domain.Models.Exceptions;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Catalogue
{
    public partial class Payment : SecurePage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        protected Lms.Domain.Models.Courses.Session session;
        string sessionId, courseId;

        protected void Page_Load(object sender, EventArgs e)
        {
            sessionId = Request.QueryString["ssid"];
            courseId = Request.QueryString["csid"];


            if (!IsPostBack)
            {
                session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);
                if (session != null)
                    CostText.Text = session.Cost.ToString();
                else
                    PayBtn.Visible = false;

                List<string> months = new List<string>();
                List<string> years = new List<string>();

                for (int i = 1; i <= 12; i++)
                {
                    months.Add(i.ToString("D2"));
                }
                ExpireMonth.DataSource = months;
                ExpireMonth.DataBind();

                for (int i = 0; i < 6; i++)
                {
                    years.Add(DateTime.UtcNow.AddYears(i).Year.ToString());
                }
                ExpireYear.DataSource = years;
                ExpireYear.DataBind();
            }
        }

        protected void PayBtn_Click(object sender, EventArgs e)
        {
            
            EnrolService.Test();
            try
            {
                logger.Info("user: {0}, session: {1}, enrol: {2}", SessionVariable.Current.User.Id, sessionId, EnrolService);

                EnrolService.ChargeSession(SessionVariable.Current.User.Id, sessionId, CardNumber.Text, ExpireYear.SelectedValue, ExpireMonth.SelectedValue, Cvv2.Text);
            }
            catch (PaymentException pex)
            {
                CustomValidator1.IsValid = false;
                CustomValidator1.ErrorMessage = pex.Message;
                CustomValidator1.Visible = true;
                return;
            }

            Response.Redirect("Details?csid=" + courseId);
        }
    }
}