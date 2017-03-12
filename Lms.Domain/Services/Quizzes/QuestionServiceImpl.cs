using Lms.Domain.Models.Quizzes;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Quizzes
{
    public class QuestionServiceImpl : IQuestionService
    {

        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected readonly IUnitOfWork unitOfWork;

        private QuestionServiceImpl()
        {

        }

        public QuestionServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public QuizQuestion GetQuestionById(string companyId, string quizId, string questionId)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                return quiz.QuizQuestions.SingleOrDefault(x => x.Id == questionId);
            }
            else
                return null;
        }


        public void DeleteQuizQuestion(string companyId, string userId, string quizId, string quizQuestionId)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                var quizQuestion = quiz.QuizQuestions.Single(x => x.Id == quizQuestionId);
                quizQuestion.IsDeleted = true;
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();
            }

        }

        public QuizQuestion CreateSingleSelectQuestion(string companyId, string quizId, string questionTitle, List<QuizAnswer> selectAnswers)
        {
            var quizQuestion = new QuizQuestion(quizId, questionTitle, selectAnswers, QuizQuestionTypeEnum.SingleSelect);
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                quiz.AddQuestion(quizQuestion);
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();

                return quizQuestion;
            }

            return null;
        }

        public QuizQuestion CreateMultiSelectQuestion(string companyId, string quizId, string questionTitle, List<QuizAnswer> selectAnswers)
        {
            var quizQuestion = new QuizQuestion(quizId, questionTitle, selectAnswers, QuizQuestionTypeEnum.MultiSelect);
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                quiz.AddQuestion(quizQuestion);
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();

                return quizQuestion;
            }

            return null;
        }

        public QuizQuestion CreateKeywordQuestion(string companyId, string quizId, string questionTitle, string keywordAnswer)
        {
            logger.Info("company: {0}, quizId: {1}, title: {2}, answer: {3}",
                companyId, quizId, questionTitle, keywordAnswer);

            var quizQuestion = new QuizQuestion(quizId, questionTitle, keywordAnswer, QuizQuestionTypeEnum.Keyword);
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                quiz.AddQuestion(quizQuestion);
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();

                return quizQuestion;
            }

            return null;
        }

        public QuizQuestion EditSingleSelectQuestion(string companyId, string quizId, string questionId, string questionTitle, List<QuizAnswer> selectAnswers)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                var question = quiz.QuizQuestions.SingleOrDefault(x => x.Id == questionId);

                if (question != null)
                {
                    question.Update(questionTitle, selectAnswers, QuizQuestionTypeEnum.SingleSelect);
                    unitOfWork.QuizRepository.Update(quiz);
                    unitOfWork.SaveChanges();
                }

                return question;
            }
            else
                return null;
        }

        public QuizQuestion EditMultiSelectQuestion(string companyId, string quizId, string questionId, string questionTitle, List<QuizAnswer> selectAnswers)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                var question = quiz.QuizQuestions.SingleOrDefault(x => x.Id == questionId);

                if (question != null)
                {
                    question.Update(questionTitle, selectAnswers, QuizQuestionTypeEnum.MultiSelect);
                    unitOfWork.QuizRepository.Update(quiz);
                    unitOfWork.SaveChanges();
                }

                return question;
            }
            else
                return null;
        }

        public QuizQuestion EditKeywordQuestion(string companyId, string quizId, string questionId, string questionTitle, string keywordAnswer)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                var question = quiz.QuizQuestions.SingleOrDefault(x => x.Id == questionId);

                if (question != null)
                {
                    question.Update(questionTitle, keywordAnswer, QuizQuestionTypeEnum.Keyword);
                    unitOfWork.QuizRepository.Update(quiz);
                    unitOfWork.SaveChanges();
                }

                return question;
            }
            else
                return null;
        }
    }
}
