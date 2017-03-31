using Lms.Domain.Models.Exceptions;
using Lms.LmsWeb.Models;
using Ninject;
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
        protected Lms.Domain.Models.Courses.Session session;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionId = Request.QueryString["ssid"];
            session = SessionService.GetSessionById(SessionVariable.Current.Company.Id, sessionId);
            if (session != null)
                CostText.Text = session.Cost.ToString();
            else
                PayBtn.Visible = false;


            if (!IsPostBack)
            {
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
            try
            {
                SessionService.ChargeSession(SessionVariable.Current.User.Id, session.Id, CardNumber.Text, ExpireYear.SelectedValue, ExpireMonth.SelectedValue, Cvv2.Text);
            }
            catch (PaymentException pex)
            {
                CustomValidator1.IsValid = false;
                CustomValidator1.ErrorMessage = pex.Message;
                CustomValidator1.Visible = true;
                return;
            }

            Response.Redirect("CourseDetails?id=" + session.CourseId);
        }
    }
}