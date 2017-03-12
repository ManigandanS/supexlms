[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Lms.LmsWeb.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Lms.LmsWeb.App_Start.NinjectWebCommon), "Stop")]

namespace Lms.LmsWeb.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Lms.Domain.Repositories;
    using Lms.Domain.Services.Companies;
    using Lms.Domain.Services.Scorms;
    using Lms.Domain.Services.Users;
    using Lms.Infrastructure.Repositories;
    using Lms.Domain.Services.Certificates;
    using Lms.Domain.Services.Courses;
    using Lms.Domain.Services.Plans;
    using Lms.Domain.Services.Quizzes;
    using Lms.Domain.Gateways.Payments;
    using Lms.Infrastructure.Gateways.Payments;
    using Lms.Domain.Services.Auths;
    using Lms.Domain.Services.Configs;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IPlanService>().To<PlanServiceImpl>();
            kernel.Bind<IPaymentService>().To<StripeServiceImpl>();
            kernel.Bind<IAuthService>().To<AuthServiceImpl>();
            kernel.Bind<ICompanyService>().To<CompanyServiceImpl>();
            kernel.Bind<IGroupService>().To<GroupServiceImpl>();
            kernel.Bind<INotificationService>().To<NotificationServiceImpl>();
            kernel.Bind<IScormService>().To<ScormServiceImpl>();
            kernel.Bind<IUserService>().To<UserServiceImpl>();
            kernel.Bind<ICertificateService>().To<CertificateServiceImpl>();
            kernel.Bind<ICourseService>().To<CourseServiceImpl>();
            kernel.Bind<IQuizService>().To<QuizServiceImpl>();
            kernel.Bind<IQuestionService>().To<QuestionServiceImpl>();
            kernel.Bind<ISessionService>().To<SessionServiceImpl>();
            kernel.Bind<ILessonService>().To<LessonServiceImpl>();
            kernel.Bind<IConfigService>().To<ConfigServiceImpl>();
        }
    }
}
