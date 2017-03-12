using Lms.Domain.Models.Quizzes;
using Lms.LmsWeb.Models;
using Lms.LmsWeb.UserControls;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Lms.LmsWeb.Course
{
    public partial class QuizLesson : SecurePage
    {
        protected string enrollmentId, lessonId;
        protected string quizId;
        protected List<object> controls = new List<object>();
        static Logger logger = LogManager.GetCurrentClassLogger();

        Domain.Models.Courses.Lesson lesson = null;
        Domain.Models.Quizzes.Quiz quiz = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            enrollmentId = Request.QueryString["enid"];
            lessonId = Request.QueryString["lsid"];


            lesson = LessonService.GetLessonById(lessonId, enrollmentId);
            quiz = lesson.Quiz;
            quizId = quiz.Id;

            var bindQuestions = quiz.LoadActiveQuizQuestions().OrderBy(x => x.Order).ToList();
            QuizNumRepeater.DataSource = bindQuestions;
            QuizNumRepeater.DataBind();

            QuestionRepeater.DataSource = bindQuestions;
            QuestionRepeater.DataBind();

        }

        protected void QuestionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var question = e.Item.DataItem as QuizQuestion;
                var answerPanel = (Panel)e.Item.FindControl("QuestionAnswer");

                switch (question.Type)
                {
                    case QuizQuestionTypeEnum.Keyword:
                        var keywordControl = (KeywordAnswer)LoadControl("~/UserControls/KeywordAnswer.ascx");
                        answerPanel.Controls.Add(keywordControl);
                        keywordControl.ID = question.Id;
                        keywordControl.Initialize(quizId, question.Id);
                        controls.Add(keywordControl);
                        break;
                    case QuizQuestionTypeEnum.MultiSelect:
                        var multiSelectControl = (MultiSelectAnswer)LoadControl("~/UserControls/MultiSelectAnswer.ascx");
                        answerPanel.Controls.Add(multiSelectControl);
                        multiSelectControl.ID = question.Id;
                        multiSelectControl.Initialize(quizId, question.Id);
                        controls.Add(multiSelectControl);
                        break;
                    case QuizQuestionTypeEnum.SingleSelect:
                        var singleSelectControl = (SingleSelectAnswer)LoadControl("~/UserControls/SingleSelectAnswer.ascx");
                        answerPanel.Controls.Add(singleSelectControl);
                        singleSelectControl.ID = question.Id;
                        singleSelectControl.Initialize(quizId, question.Id);
                        controls.Add(singleSelectControl);
                        break;
                }
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            List<UserAnswer> userAnswers = new List<UserAnswer>();

            logger.Debug("controls length: " + controls.Count);
            foreach (var item in controls)
            {
                if (item is KeywordAnswer)
                {
                    string questionId = ((KeywordAnswer)item).ID;
                    string answer = ((KeywordAnswer)item).Answer;
                    userAnswers.Add(new UserAnswer() { QuizId = quizId, QuizQuestionId = questionId, QuizUserAnswer = answer });

                    logger.Info("userId: {2}, KeywordAnswer questionId: {0}, answer: {1}", questionId, answer, SessionVariable.Current.User.Id);
                }

                if (item is SingleSelectAnswer)
                {
                    string questionId = ((SingleSelectAnswer)item).ID;
                    string answer = ((SingleSelectAnswer)item).Answer;
                    userAnswers.Add(new UserAnswer() { QuizId = quizId, QuizQuestionId = questionId, QuizUserAnswer = answer });

                    logger.Info("userId: {2}, SingleSelectAnswer questionId: {0}, answer: {1}", questionId, answer, SessionVariable.Current.User.Id);
                }

                if (item is MultiSelectAnswer)
                {
                    string questionId = ((MultiSelectAnswer)item).ID;
                    string answer = ((MultiSelectAnswer)item).Answer;
                    userAnswers.Add(new UserAnswer() { QuizId = quizId, QuizQuestionId = questionId, QuizUserAnswer = answer });

                    logger.Info("userId: {2}, MultiSelectAnswer questionId: {0}, answer: {1}", questionId, answer, SessionVariable.Current.User.Id);
                }
            }

            try
            {
                LessonService.Grade(SessionVariable.Current.User.Id, quizId, lessonId, enrollmentId, userAnswers);
            }
            catch (DbEntityValidationException dvex)
            {
                foreach (var eve in dvex.EntityValidationErrors)
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


            Response.Redirect("Curriculum?enid=" + enrollmentId);
        }
    }
}