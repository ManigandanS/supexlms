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
    public partial class CM0001 : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var company = SessionVariable.Current.Company;

            CompanyName.Text = company.Name;
            TrialCompany.Text = company.IsTrial.ToString();
            Expiry.Text = company.Expiry.ToString();
        }
    }
}