using Ninject.WebContext;
using System.Web.Http;
using System.Web.Mvc;

namespace Ninject.CoreContext
{
    /// <summary>
    /// Ninject Context extension
    /// </summary>
    public static class NinjectContextExtension
    {
        /// <summary>
        /// Ninject dependency resolver
        /// </summary>
        static NinjectDependencyResolver _ninjectDependencyResolver;

        /// <summary>
        /// True if WebApi is initialized, otherwise false.
        /// </summary>
        static bool _webApiInitialized;

        /// <summary>
		/// True if Mvc is initialized, otherwise false.
		/// </summary>
		static bool _mvcInitialized;

        /// <summary>
        /// Initialize dependency resolver
        /// </summary>
        /// <param name="kernel"></param>
        static void InitializeDependencyResovler(IKernel kernel)
        {
            if(_ninjectDependencyResolver == null)
                _ninjectDependencyResolver = new NinjectDependencyResolver(kernel);
        }

        /// <summary>
		/// Uses MVC.
		/// </summary>
		public static NinjectContext UseMvc(this NinjectContext context)
        {
            if (!_mvcInitialized)
            {   
                context.Use(kernel => 
                {
                    InitializeDependencyResovler(kernel);
                    kernel.Bind<IActionInvoker>().To<NinjectControllerActionInvoker>().InTransientScope();
                    DependencyResolver.SetResolver(_ninjectDependencyResolver);
                });

                _mvcInitialized = true;
            }
            return context;
        }

        /// <summary>
        /// Use WebApi.
        /// </summary>
        public static NinjectContext UseWebApi(this NinjectContext context)
        {
            if (!_webApiInitialized)
            {
                context.Use(kernel =>
                {
                    InitializeDependencyResovler(kernel);
                    GlobalConfiguration.Configuration.DependencyResolver = _ninjectDependencyResolver;
                    GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Filters.IFilterProvider), new NinjectFilterProvider(kernel));
                });
                _webApiInitialized = true;
            }
            return context;
        }
    }
}
