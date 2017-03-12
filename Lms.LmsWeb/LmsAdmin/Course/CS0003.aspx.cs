using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0003 : AdminPage
    {
        string courseId;

        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request.QueryString["csid"];

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    var course = CourseService.GetCourseById(SessionVariable.Current.Company.Id, courseId);

                    if (course != null)
                    {
                        if (course.Sessions.SelectMany(x => x.Enrollments).Any())
                            DelBtn.Attributes.Add("disabled", "disabled");

                        NameTextBox.Text = course.Name;
                        DescriptionTextBox.Text = course.Description;

                        CourseTypeRadioButtonList.SelectedValue = ((int)course.CourseType).ToString();
                        CourseLocationRadioButtonList.SelectedValue = ((int)course.CourseLocation).ToString();
                        CourseAccessDropDownList.SelectedValue = ((int)course.CourseAccess).ToString();
                        CourseTypeRadioButtonList.Enabled = false;

                        CertificateDropDownList.DataValueField = "Id";
                        CertificateDropDownList.DataTextField = "Name";
                        CertificateDropDownList.DataSource = CertificateService.LoadAllCertificates(SessionVariable.Current.Company.Id);
                        CertificateDropDownList.DataBind();
                        CertificateDropDownList.Items.Insert(0, new ListItem { Text = "Select a certificate", Value = "0" });

                        if (course.CertificateId != null)
                            CertificateDropDownList.SelectedValue = course.CertificateId;
                    }
                }
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (CertificateDropDownList.SelectedValue == "0")
            {
                CourseService.EditCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                    courseId, NameTextBox.Text, DescriptionTextBox.Text,
                    CourseTypeRadioButtonList.SelectedValue, CourseLocationRadioButtonList.SelectedValue,
                    CourseAccessDropDownList.SelectedValue);
            }
            else
            {
                CourseService.EditCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                    courseId, NameTextBox.Text, DescriptionTextBox.Text,
                    CourseTypeRadioButtonList.SelectedValue, CourseLocationRadioButtonList.SelectedValue,
                    CourseAccessDropDownList.SelectedValue, CertificateDropDownList.SelectedValue);
            }

            Response.Redirect("CS0001");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            CourseService.DeleteCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId);

            Response.Redirect("CS0001");
        }
    }
}