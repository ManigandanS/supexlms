[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Lms.LmsWeb.App_Start.NinjectWeb), "Start")]

namespace Lms.LmsWeb.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject.Web;

    public static class NinjectWeb 
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}
