using Lms.Domain.Models.Quizzes;
using Lms.Domain.Models.Users;
using Lms.Domain.Repositories;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Quizzes
{
    public class QuizServiceImpl : IQuizService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected readonly IUnitOfWork unitOfWork;

        private QuizServiceImpl()
        {

        }

        public QuizServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        

        public IEnumerable<Quiz> LoadAllQuizzes(string companyId)
        {
            return unitOfWork.QuizRepository.GetAll().Where(x => x.CompanyId == companyId && !x.IsDeleted);
        }

        public IEnumerable<Quiz> LoadPublishedQuizzes(string companyId)
        {
            return unitOfWork.QuizRepository.GetAll().Where(x => x.CompanyId == companyId && !x.IsDeleted && x.IsPublished);
        }

        public Quiz GetQuizById(string companyId, string quizId)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
                return quiz;
            else
                return null;
        }

        public void DeleteQuiz(string companyId, string userId, string quizId)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                quiz.Delete();
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();
            }
        }

        public void PublishQuiz(string companyId, string userId, string quizId)
        {
            var quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (quiz.CompanyId == companyId)
            {
                quiz.Publish();
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();
            }
        }

        public Quiz CreateQuiz(string companyId, string userId, string name, string description, float passPercent)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            Quiz quiz = new Quiz(userId, name, description, passPercent);
            company.AddQuiz(quiz);

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();

            return quiz;
        }

        public Quiz EditQuiz(string companyId, string userId, string quizId, string name, string description, float passPercent)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            Quiz quiz = unitOfWork.QuizRepository.GetById(quizId);
            if (company.Id == quiz.CompanyId)
            {
                quiz.UpdateQuiz(name, description, passPercent);
                unitOfWork.QuizRepository.Update(quiz);
                unitOfWork.SaveChanges();
            }

            return quiz;
        }
    }
}
