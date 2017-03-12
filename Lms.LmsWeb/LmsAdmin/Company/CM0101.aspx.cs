using Lms.Domain.Services.Companies;
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
    public partial class CM0101 : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GroupRepeater.DataSource = GroupService.LoadAllGroups(SessionVariable.Current.Company.Id);
                GroupRepeater.DataBind();
            }
        }

    }
}