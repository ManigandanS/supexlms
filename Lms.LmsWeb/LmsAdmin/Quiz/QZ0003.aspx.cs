using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Quiz
{
    public partial class QZ0003 : AdminPage
    {
        string quizId;

        protected void Page_Load(object sender, EventArgs e)
        {
            quizId = Request.QueryString["id"];

            if (!IsPostBack)
            {
                var quiz = QuizService.GetQuizById(SessionVariable.Current.Company.Id, quizId);

                if (quiz != null)
                {
                    if (quiz.Lessons.Any(x => !x.IsDeleted))
                        DelBtn.Attributes.Add("disabled", "disabled");

                    NameTextBox.Text = quiz.Title;
                    DescriptionTextBox.Text = quiz.Description;
                    PassPercentTextBox.Text = quiz.PassPercent.ToString();
                }
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            QuizService.EditQuiz(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, quizId,
                    NameTextBox.Text, DescriptionTextBox.Text, float.Parse(PassPercentTextBox.Text));

            Response.Redirect(Request.RawUrl);
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            QuizService.DeleteQuiz(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, quizId);

            Response.Redirect("QZ0001");
        }
    }
}