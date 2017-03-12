using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Course
{
    public partial class Catalogue : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CourseRepeater.DataSource = CourseService.LoadAllPublishedCourses(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id).ToList();
                CourseRepeater.DataBind();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            var courses = CourseService.LoadAllPublishedCourses(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id)
                .Where(x => x.Name.Contains(SearchText.Text) || x.Description.Contains(SearchText.Text)).ToList();

            CourseRepeater.DataSource = courses;
            CourseRepeater.DataBind();
        }
    }
}