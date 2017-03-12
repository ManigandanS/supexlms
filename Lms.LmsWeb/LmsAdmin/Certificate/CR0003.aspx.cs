using Lms.Domain.Models.Certificates;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Certificate
{
    public partial class CR0003 : AdminPage
    {
        protected string certificateId;
        protected bool showExpiryPanel = false;
        static Logger logger = LogManager.GetCurrentClassLogger();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            certificateId = Request.QueryString["id"];

            if (!IsPostBack)
            {
                var certificate = CertificateService.GetCertificateById(certificateId, SessionVariable.Current.Company.Id);

                if (certificate != null)
                {
                    Name.Text = certificate.Name;
                    Description.Text = certificate.Description;
                    ExpiryType.SelectedValue = ((int)certificate.ExpiryType).ToString();
                    ExpiryMonth.Text = certificate.ExpiryMonth.ToString();

                    if (certificate.ExpiryType == CertificateExpiryType.Term)
                        showExpiryPanel = true;

                    CourseRepeater.DataSource = certificate.GetAttachedCourses().ToList();
                    CourseRepeater.DataBind();
                }
            }
        }


        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    logger.Info("EDIT CERTIFICATE: [name: {0}], [desc: {1}], [user: {2}], [company: {3}]",
                        Name.Text, Description.Text, SessionVariable.Current.User.Id, SessionVariable.Current.Company.Id);

                    CertificateExpiryType expiryType = (CertificateExpiryType)Convert.ToInt32(ExpiryType.SelectedValue);
                    int? expiryMonth = null;
                    if (ExpiryType.SelectedValue == "1")
                        expiryMonth = Convert.ToInt32(ExpiryMonth.Text);

                    CertificateService.EditCertificate(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                        certificateId, Name.Text, Description.Text, expiryType, expiryMonth);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }

            }

            Response.Redirect("CR0001");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            CertificateService.DeleteCertificate(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, certificateId);

            Response.Redirect("CR0001");
        }
    }
}