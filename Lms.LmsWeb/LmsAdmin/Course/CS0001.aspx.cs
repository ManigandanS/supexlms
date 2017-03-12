using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Course
{
    public partial class CS0001 : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CourseRepeater.DataSource = CourseService.LoadAllCourses(SessionVariable.Current.Company.Id).OrderBy(x => x.Name).ToList();
                CourseRepeater.DataBind();
            }
        }


        protected void PublishBtn_Command(object sender, CommandEventArgs e)
        {
            string courseId = e.CommandArgument as string;
            CourseService.PublishCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId);

            Response.Redirect(Request.RawUrl);
        }

        protected void UnpublishBtn_Command(object sender, CommandEventArgs e)
        {
            string courseId = e.CommandArgument as string;
            CourseService.UnpublishCourse(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, courseId);

            Response.Redirect(Request.RawUrl);
        }

        protected void CourseRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var course = (Domain.Models.Courses.Course)e.Item.DataItem;
                Button publishBtn = (Button)e.Item.FindControl("PublishBtn");

                if (course.IsPublished)
                {
                    publishBtn.Attributes.Add("disabled", "disabled");
                }
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }

        protected void CurriculumBtn_Command(object sender, CommandEventArgs e)
        {
            string courseId = e.CommandArgument as string;
            Response.Redirect("CS0101?csid=" + courseId);
        }
    }
}