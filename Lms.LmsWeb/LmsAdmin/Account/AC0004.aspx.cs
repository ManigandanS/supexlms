using Lms.Domain.Models.Courses;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Account
{
    public partial class AC0004 : AdminPage
    {
        protected string enrollmentId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            enrollmentId = Request.QueryString["enid"];
            
            if (!IsPostBack)
            {
                var enrollment = UserService.GetEnrollment(enrollmentId);

                if (enrollment != null)
                {
                    LessonRepeater.DataSource = enrollment.ScormData.OrderBy(x => x.Lesson.Order);
                    LessonRepeater.DataBind();
                }
            }
        }

        
    }
}