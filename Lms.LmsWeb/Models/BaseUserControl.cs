using Lms.Domain.Repositories;
using Lms.Domain.Services.Quizzes;
using Ninject;
using Ninject.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lms.LmsWeb.Models
{
    public class BaseUserControl : UserControlBase
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }
        [Inject]
        public IQuizService QuizService { get; set; }
    }
}