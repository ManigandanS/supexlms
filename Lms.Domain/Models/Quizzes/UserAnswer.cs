using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Quizzes
{
    public class UserAnswer
    {
        public string QuizId { get; set; }
        public string QuizQuestionId { get; set; }
        public string QuizUserAnswer { get; set; }
    }
}
