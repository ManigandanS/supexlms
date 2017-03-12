using Lms.Domain.Models.Certificates;
using Lms.Domain.Services.Certificates;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Certificate
{
    public partial class CR0002 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    logger.Debug("SessionVariable.Current: " + SessionVariable.Current.User);

                    logger.Info("NEW CERTIFICATE: [name: {0}], [desc: {1}], [user: {2}], [company: {3}]",
                        Name.Text, Description.Text, SessionVariable.Current.User.Id, SessionVariable.Current.Company.Id);

                    CertificateExpiryType expiryType = (CertificateExpiryType)Convert.ToInt32(ExpiryType.SelectedValue);
                    int? expiryMonth = null;
                    if (ExpiryType.SelectedValue == "1")
                        expiryMonth = Convert.ToInt32(ExpiryMonth.Text);

                    CertificateService.CreateCertificate(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                        Name.Text, Description.Text, expiryType, expiryMonth);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }

            }

            Response.Redirect("CR0001");
        }
    }
}