using Lms.Domain.Models.Quizzes;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
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
    public partial class QZ0103 : AdminPage
    {
        protected string quizId, questionId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            quizId = Request.QueryString["qzid"];
            questionId = Request.QueryString["qsid"];            

            if (!IsPostBack)
            {
                var question = QuestionService.GetQuestionById(SessionVariable.Current.Company.Id, quizId, questionId);

                if (question != null)
                {
                    TitleTextBox.Text = question.Title;
                    TypeRadio.SelectedValue = ((int)question.Type).ToString();

                    switch (TypeRadio.SelectedValue)
                    {
                        case "0": // single select
                            BindSingleSelectAnswer(question);
                            break;
                        case "1": // multi select
                            BindMultiSelectAnswer(question);
                            break;
                        case "2": // keyword
                            BindKeywordAnswer(question);
                            break;
                        default:
                            break;
                    }

                    if (question.Quiz.IsPublished)
                    {
                        DelBtn.Visible = false;
                        EditBtn.Visible = false;
                    }
                }
            }
        }

        private void BindSingleSelectAnswer(QuizQuestion question)
        {
            var answers = question.LoadActiveQuizAnswer().ToList();

            for (int i = 0; i < answers.Count; i++)
            {
                var radio = (RadioButton)this.Master.FindControl("MainContent").FindControl("SingleSelectRadio" + (i + 1));
                var answer = (TextBox)this.Master.FindControl("MainContent").FindControl("SingleSelectAnswer" + (i + 1));

                answer.Text = answers[i].Title;
                if (answers[i].IsRight)
                    radio.Checked = true;

            }
        }

        private void BindMultiSelectAnswer(QuizQuestion question)
        {
            var answers = question.LoadActiveQuizAnswer().ToList();

            for (int i = 0; i < answers.Count; i++)
            {
                var checkbox = (CheckBox)this.Master.FindControl("MainContent").FindControl("MultiSelectCheck" + (i + 1));
                var answer = (TextBox)this.Master.FindControl("MainContent").FindControl("MultiSelectAnswer" + (i + 1));

                answer.Text = answers[i].Title;
                if (answers[i].IsRight)
                    checkbox.Checked = true;

            }
        }

        private void BindKeywordAnswer(QuizQuestion question)
        {
            KeywordAnswer.Text = question.LoadActiveQuizAnswer().First().Title;
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                switch (TypeRadio.SelectedValue)
                {
                    case "0": // single select
                        EditSingleSelectAnswer();
                        break;
                    case "1": // multi select
                        EditMultiSelectAnswer();
                        break;
                    case "2": // keyword
                        EditKeywordAnswer();
                        break;
                    default:
                        break;
                }
            }

            Response.Redirect("QZ0101?qzid=" + quizId);
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            QuestionService.DeleteQuizQuestion(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, quizId, questionId);
        }

        private void EditSingleSelectAnswer()
        {
            List<QuizAnswer> selectAnswers = new List<QuizAnswer>();
            int order = 1;

            for (int i = 1; i <= 8; i++)
            {

                var radio = (RadioButton)this.Master.FindControl("MainContent").FindControl("SingleSelectRadio" + i);
                var answer = (TextBox)this.Master.FindControl("MainContent").FindControl("SingleSelectAnswer" + i);


                if (radio.Checked)
                {
                    if (!string.IsNullOrEmpty(answer.Text))
                    {
                        selectAnswers.Add(new QuizAnswer
                            {
                                IsRight = true,
                                Title = answer.Text,
                                Order = order
                            });
                    }
                } 
                else
                {
                    if (!string.IsNullOrEmpty(answer.Text))
                    {
                        selectAnswers.Add(new QuizAnswer
                        {
                            IsRight = false,
                            Title = answer.Text,
                            Order = order
                        });
                    }
                }

                order++;
            }

            try
            {
                QuestionService.EditSingleSelectQuestion(SessionVariable.Current.Company.Id, quizId, questionId, TitleTextBox.Text, selectAnswers);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private void EditMultiSelectAnswer()
        {
            List<QuizAnswer> selectAnswers = new List<QuizAnswer>();
            int order = 1;

            for (int i = 1; i <= 8; i++)
            {
                var checkbox = (CheckBox)this.Master.FindControl("MainContent").FindControl("MultiSelectCheck" + i);
                var answer = (TextBox)this.Master.FindControl("MainContent").FindControl("MultiSelectAnswer" + i);

                if (checkbox.Checked)
                {
                    if (!string.IsNullOrEmpty(answer.Text))
                    {
                        selectAnswers.Add(new QuizAnswer
                        {
                            IsRight = true,
                            Title = answer.Text,
                            Order = order
                        });
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(answer.Text))
                    {
                        selectAnswers.Add(new QuizAnswer
                        {
                            IsRight = false,
                            Title = answer.Text,
                            Order = order
                        });
                    }
                }

                order++;
            }

            QuestionService.EditMultiSelectQuestion(SessionVariable.Current.Company.Id, quizId, questionId, TitleTextBox.Text, selectAnswers);
        }

        private void EditKeywordAnswer()
        {
            QuestionService.EditKeywordQuestion(SessionVariable.Current.Company.Id, quizId, questionId, TitleTextBox.Text, KeywordAnswer.Text);
        }
    }
}