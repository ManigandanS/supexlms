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


        protected void PublishBtn_Command(object sender, CommandEventArgs e)
        {
            string quizId = e.CommandArgument as string;
            QuizService.PublishQuiz(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, quizId);

            Response.Redirect(Request.RawUrl);
        }

        
        protected void QuizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                

            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}