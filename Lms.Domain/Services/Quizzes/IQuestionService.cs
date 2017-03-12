using Lms.Domain.Models.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Quizzes
{
    public interface IQuestionService
    {
        QuizQuestion CreateSingleSelectQuestion(string companyId, string quizId, string questionTitle, List<QuizAnswer> selectAnswers);
        QuizQuestion CreateMultiSelectQuestion(string companyId, string quizId, string questionTitle, List<QuizAnswer> selectAnswers);
        QuizQuestion CreateKeywordQuestion(string companyId, string quizId, string questionTitle, string keywordAnswer);
        void DeleteQuizQuestion(string companyId, string userId, string quizId, string quizQuestionId);
        QuizQuestion GetQuestionById(string companyId, string quizId, string questionId);

        QuizQuestion EditSingleSelectQuestion(string companyId, string quizId, string questionId, string questionTitle, List<QuizAnswer> selectAnswers);
        QuizQuestion EditMultiSelectQuestion(string companyId, string quizId, string questionId, string questionTitle, List<QuizAnswer> selectAnswers);
        QuizQuestion EditKeywordQuestion(string companyId, string quizId, string questionId, string questionTitle, string keywordAnswer);
    }
}
