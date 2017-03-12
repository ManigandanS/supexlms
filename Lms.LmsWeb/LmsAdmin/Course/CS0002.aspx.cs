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

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0002 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CertificateDropDownList.DataValueField = "Id";
                CertificateDropDownList.DataTextField = "Name";
                CertificateDropDownList.DataSource = CertificateService.LoadAllCertificates(SessionVariable.Current.Company.Id);
                CertificateDropDownList.DataBind();
                CertificateDropDownList.Items.Insert(0, new ListItem { Text = "Select a certificate", Value = "0" });
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (CertificateDropDownList.SelectedValue == "0")
            {
                CourseService.CreateCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                    NameTextBox.Text, DescriptionTextBox.Text,
                    CourseTypeRadioButtonList.SelectedValue, CourseLocationRadioButtonList.SelectedValue,
                    CourseAccessDropDownList.SelectedValue);
            }
            else
            {
                CourseService.CreateCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, 
                    NameTextBox.Text, DescriptionTextBox.Text,
                    CourseTypeRadioButtonList.SelectedValue, CourseLocationRadioButtonList.SelectedValue,
                    CourseAccessDropDownList.SelectedValue, CertificateDropDownList.SelectedValue);
            }

            Response.Redirect("CS0001");
        }
    }
}