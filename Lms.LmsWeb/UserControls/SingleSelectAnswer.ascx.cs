using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.UserControls
{
    public partial class SingleSelectAnswer : BaseUserControl
    {

        static Logger logger = LogManager.GetCurrentClassLogger();
        protected string quizId, questionId;

        public string Answer
        {
            get { return UserAnswerRadio.SelectedValue; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            string companyId = SessionVariable.Current.Company.Id;

            logger.Debug("quizId: {0}, questionId: {1}, companyId: {2}, obj: {3}", quizId, questionId, companyId, QuizService);

            var quiz = QuizService.GetQuizById(companyId, quizId);
            if (quiz != null)
            {
                var quizQuestion = quiz.LoadActiveQuizQuestions().SingleOrDefault(x => x.Id == questionId);

                if (quizQuestion != null)
                {
                    foreach (var quizAnswer in quizQuestion.LoadActiveQuizAnswer())
                    {
                        UserAnswerRadio.Items.Add(new ListItem() { Text = quizAnswer.Title, Value = quizAnswer.Id });
                    }
                }
            }
        }

        public void Initialize(string quizId, string questionId)
        {
            this.quizId = quizId;
            this.questionId = questionId;
        }
    }
}