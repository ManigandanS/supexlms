using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Content
{
    public partial class CN0003 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        protected string scormId;

        protected void Page_Load(object sender, EventArgs e)
        {
            scormId = Request.QueryString["id"];

            if (!IsPostBack)
            {
                var scorm = ScormService.GetScormById(scormId, SessionVariable.Current.Company.Id);

                if (scorm != null)
                {
                    Name.Text = scorm.Name;
                    Description.Text = scorm.Description;
                    Manifest.Text = HttpUtility.HtmlEncode(scorm.ManifestXml).Trim();

                    if (scorm.Lessons.Any(x => !x.IsDeleted))
                        DelBtn.Attributes.Add("disabled", "disabled");

                    logger.Debug(Manifest.Text);
                }
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                
                ScormService.EditScorm(SessionVariable.Current.User.Id, SessionVariable.Current.Company.Id,
                    scormId, Name.Text, Description.Text);

                Response.Redirect(Request.RawUrl);
            }
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            ScormService.DeleteScorm(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, scormId);

            Response.Redirect("CN0001");
        }
    }
}