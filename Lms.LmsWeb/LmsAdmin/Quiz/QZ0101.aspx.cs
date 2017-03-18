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
    public partial class QZ0101 : AdminPage
    {
        protected Lms.Domain.Models.Quizzes.Quiz quiz;

        protected void Page_Load(object sender, EventArgs e)
        {
            quiz = QuizService.GetQuizById(SessionVariable.Current.Company.Id, Request.QueryString["qzid"]);

            if (!IsPostBack && quiz != null)
            {
                CreateLink.NavigateUrl = "QZ0102?qzid=" + quiz.Id;

                QuestionRepeater.DataSource = QuizService.GetQuizById(SessionVariable.Current.Company.Id, quiz.Id).LoadActiveQuizQuestions().ToList();
                QuestionRepeater.DataBind();
            }
        }

        protected void DelBtn_Command(object sender, CommandEventArgs e)
        {
            string quizQuestionId = e.CommandArgument as string;
            QuestionService.DeleteQuizQuestion(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, quiz.Id, quizQuestionId);

            Response.Redirect(Request.RawUrl);
        }

        
        protected void QuizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                

            }
        }

    }
}