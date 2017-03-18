using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.Catalogue
{
    public partial class Index : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CourseRepeater.DataSource = CourseService.LoadAllCourses(SessionVariable.Current.Company.Id).ToList();
                CourseRepeater.DataBind();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            Func<Lms.Domain.Models.Courses.Course, bool> nameFilter = x => x.Name.Contains(SearchText.Text);
            Func<Lms.Domain.Models.Courses.Course, bool> descFilter = x => x.Description.Contains(SearchText.Text);
            Func<Lms.Domain.Models.Courses.Course, bool> predicate = x => nameFilter(x) || descFilter(x);

            var courses = CourseService.FindCourses(SessionVariable.Current.Company.Id, predicate);

            CourseRepeater.DataSource = courses;
            CourseRepeater.DataBind();
        }
    }
}