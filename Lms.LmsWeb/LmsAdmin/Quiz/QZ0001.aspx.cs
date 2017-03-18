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

namespace Lms.LmsWeb.LmsAdmin.Quiz
{
    public partial class QZ0001 : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QuizRepeater.DataSource = QuizService.LoadAllQuizzes(SessionVariable.Current.Company.Id).ToList();
                QuizRepeater.DataBind();
            }
        }


        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}