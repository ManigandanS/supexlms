using Lms.Domain.Models.Quizzes;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Lms.LmsWeb.UserControls;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Quiz
{
    public partial class QZ0104 : AdminPage
    {
        protected string questionId, quizId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            quizId = Request.QueryString["qzid"];
            questionId = Request.QueryString["qsid"];

            var question = QuestionService.GetQuestionById(SessionVariable.Current.Company.Id, quizId, questionId);

            if (question != null)
            {
                QuestionTitle.Text = question.Title;

                switch (question.Type)
                {
                    case QuizQuestionTypeEnum.Keyword:
                        var keywordControl = (KeywordAnswer)LoadControl("~/UserControls/KeywordAnswer.ascx");
                        AnswerPanel.Controls.Add(keywordControl);
                        keywordControl.ID = question.Id;
                        keywordControl.Initialize(quizId, question.Id);
                        break;
                    case QuizQuestionTypeEnum.MultiSelect:
                        var multiSelectControl = (MultiSelectAnswer)LoadControl("~/UserControls/MultiSelectAnswer.ascx");
                        AnswerPanel.Controls.Add(multiSelectControl);
                        multiSelectControl.ID = question.Id;
                        multiSelectControl.Initialize(quizId, question.Id);
                        break;
                    case QuizQuestionTypeEnum.SingleSelect:
                        var singleSelectControl = (SingleSelectAnswer)LoadControl("~/UserControls/SingleSelectAnswer.ascx");
                        AnswerPanel.Controls.Add(singleSelectControl);
                        singleSelectControl.ID = question.Id;
                        singleSelectControl.Initialize(quizId, question.Id);
                        break;
                }
            }
        }
    }
}