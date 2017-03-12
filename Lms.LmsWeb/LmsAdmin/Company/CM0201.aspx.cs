using Lms.Domain.Gateways.Payments;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Services.Plans;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Company
{
    public partial class CM0201 : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlanDropDown.DataTextField = "Name";
                PlanDropDown.DataValueField = "Id";
                PlanDropDown.DataSource = PlanService.LoadActivePlans().ToList();
                PlanDropDown.DataBind();

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
                var planId = PlanDropDown.SelectedValue;
                PaymentService.ChargePlan(SessionVariable.Current.Company.Id, planId, CardNumber.Text, ExpireYear.SelectedValue, ExpireMonth.SelectedValue, Cvv2.Text);
            }
            catch (PaymentException pex)
            {
                CustomValidator1.IsValid = false;
                CustomValidator1.ErrorMessage = pex.Message;
                CustomValidator1.Visible = true;
                return;
            }

            Response.Redirect("CM0001");
        }

    }
}