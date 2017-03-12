using Lms.Domain.Services.Certificates;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Certificate
{
    public partial class CR0001 : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchControl1.SearchButtonClick += new EventHandler(SearchBtn_Click);

            if (!IsPostBack)
            {
                Repeater1.DataSource = CertificateService.LoadAllCertificates(SessionVariable.Current.Company.Id);
                Repeater1.DataBind();
            }
        }


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //Response.Write(SearchControl1.Keyword);
        }
    }
}