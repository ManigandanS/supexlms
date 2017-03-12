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
    public partial class QZ0102 : AdminPage
    {
        protected string quizId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            quizId = Request.QueryString["qzid"];

            if (!IsPostBack)
            {
                
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                switch (TypeRadio.SelectedValue)
                {
                    case "0": // single select
                        CreateSingleSelectAnswer();
                        break;
                    case "1": // multi select
                        CreateMultiSelectAnswer();
                        break;
                    case "2": // keyword
                        CreateKeywordAnswer();
                        break;
                    default:
                        break;
                }
            }

            Response.Redirect("QZ0101?qzid=" + quizId);
        }

        private void CreateSingleSelectAnswer()
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
                QuestionService.CreateSingleSelectQuestion(SessionVariable.Current.Company.Id, quizId, TitleTextBox.Text, selectAnswers);
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

        private void CreateMultiSelectAnswer()
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

            QuestionService.CreateMultiSelectQuestion(SessionVariable.Current.Company.Id, quizId, TitleTextBox.Text, selectAnswers);
        }

        private void CreateKeywordAnswer()
        {
            QuestionService.CreateKeywordQuestion(SessionVariable.Current.Company.Id, quizId, TitleTextBox.Text, KeywordAnswer.Text);
        }
    }
}