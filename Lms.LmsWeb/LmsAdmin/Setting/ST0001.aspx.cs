using Lms.Domain.Models.Commons;
using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Setting
{
    public partial class ST0001 : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigRepeater.DataSource = this.UnitOfWork.ConfigurationRepository.GetAll().ToList();
                ConfigRepeater.DataBind();
            }
        }

        protected void ConfigRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var config = e.Item.DataItem as Configuration;
                bool hasConfig = config.CompanyConfigurations.Where(x => x.CompanyId == SessionVariable.Current.Company.Id).Any();
                Literal use = (Literal)e.Item.FindControl("Use");

                if (hasConfig)
                    use.Text = "Yes";
                else
                    use.Text = "No";

            }
        }
    }
}