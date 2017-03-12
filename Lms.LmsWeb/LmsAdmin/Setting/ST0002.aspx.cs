using Lms.Domain.Models.Commons;
using Lms.Domain.Models.Companies;
using Lms.LmsWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Setting
{
    public partial class ST0002 : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var configJson = ConfigService.GetCompanyConfigJsonById(SessionVariable.Current.Company.Id, Configuration.OFFICE365_CODE);

                if (configJson != null)
                {
                    if (configJson["endPoint"] != null)
                    {
                        EndPointTextBox.Text = (String)configJson["endPoint"];
                    }

                    if (configJson["appID"] != null)
                    {
                        AppIDTextBox.Text = (String)configJson["appID"];
                    }
                }
                else
                {
                    DelBtn.Visible = false;
                }
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {

            ConfigService.RemoveCompanyConfig(SessionVariable.Current.Company.Id, Configuration.OFFICE365_CODE);

            JObject jsonObj = new JObject();
            jsonObj["endPoint"] = EndPointTextBox.Text;
            jsonObj["appID"] = AppIDTextBox.Text;

            string json = jsonObj.ToString();
            ConfigService.AddCompanyConfig(SessionVariable.Current.Company.Id, Configuration.OFFICE365_CODE, json);

            Response.Redirect("ST0001");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            ConfigService.RemoveCompanyConfig(SessionVariable.Current.Company.Id, Configuration.OFFICE365_CODE);
            Response.Redirect("ST0001");
        }
    }
}