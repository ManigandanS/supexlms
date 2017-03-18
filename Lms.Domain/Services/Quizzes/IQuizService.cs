using Lms.Domain.Models.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Quizzes
{
    public interface IQuizService
    {
        IEnumerable<Quiz> LoadAllQuizzes(string companyId);
        Quiz GetQuizById(string companyId, string quizId);
        void DeleteQuiz(string companyId, string userId, string quizId);
        Quiz CreateQuiz(string companyId, string userId, string name, string description, float passPercent);
        Quiz EditQuiz(string companyId, string userId, string quizId, string name, string description, float passPercent);
        
    }
}
