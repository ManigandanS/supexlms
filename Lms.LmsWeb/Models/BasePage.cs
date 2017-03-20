using Lms.Domain.Repositories;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Lms.Domain.Services.Companies;
using Lms.Domain.Services.Users;
using Lms.Domain.Services.Courses;
using Lms.Domain.Services.Certificates;
using Lms.Domain.Services.Scorms;
using Lms.Domain.Services.Quizzes;
using Ninject.Web;
using Lms.Domain.Gateways.Payments;
using Lms.Domain.Services.Plans;
using Lms.Domain.Services.Auths;
using Lms.Domain.Services.Configs;
using Lms.Domain.Services.Enrolments;

namespace Lms.LmsWeb.Models
{
    public class BasePage : PageBase
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public ICompanyService CompanyService { get; set; }
        [Inject]
        public IGroupService GroupService { get; set; }
        [Inject]
        public INotificationService NotificationService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public ICourseService CourseService { get; set; }
        [Inject]
        public ICertificateService CertificateService { get; set; }
        [Inject]
        public IScormService ScormService { get; set; }
        [Inject]
        public IQuizService QuizService { get; set; }        
        [Inject]
        public ISessionService SessionService { get; set; }
        [Inject]
        public ILessonService LessonService { get; set; }
        [Inject]
        public IQuestionService QuestionService { get; set; }
        [Inject]
        public IPlanService PlanService { get; set; }
        [Inject]
        public IPaymentService PaymentService { get; set; }
        [Inject]
        public IConfigService ConfigService { get; set; }
        [Inject]
        public IEnrolService EnrolService { get; set; }


        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        protected void AuthenticateUser()
        {
            logger.Trace(Request.IsAuthenticated + " --- " + User.Identity.IsAuthenticated);

            if (!User.Identity.IsAuthenticated)               
                Response.Redirect("~/Pages/Account/Login");
            
                


            string userId = string.Empty, uri = string.Empty;

            try
            {
                IEnumerable<Claim> claims = Request.GetOwinContext().Authentication.User.Claims;

                userId = Context.User.Identity.GetUserName();
                uri = claims.FirstOrDefault(x => x.Type == ClaimTypes.Uri).Value;
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                Response.Redirect("~/Pages/Account/Login");
            }


            logger.Debug("[userId: {0}], [uri: {1}]", userId, uri);


            if (uri != Request.Url.Host)
                Response.Redirect("~/Pages/Account/Login");


            if (SessionVariable.Current.User == null || SessionVariable.Current.Company == null)
            {
                var user = UnitOfWork.UserRepository.GetById(userId);
                logger.Debug("user: " + user);
                SessionVariable.Current.User = user;

                var company = UnitOfWork.CompanyRepository.GetAll().Single(x => x.HostName == Request.Url.Host);
                logger.Debug("company: " + company);
                SessionVariable.Current.Company = company;

                SessionVariable.Current.UserGroups = user.UserGroups.ToList();
            }
        }

        public bool IsCompanyAdmin()
        {
            var adminGroup = SessionVariable.Current.Company.Groups.SingleOrDefault(x => x.Name == "Administrator");
            if (adminGroup != null)
            {
                return SessionVariable.Current.User.UserGroups.Any(x => x.GroupId == adminGroup.Id);
            }

            return false;
        }

    }
}