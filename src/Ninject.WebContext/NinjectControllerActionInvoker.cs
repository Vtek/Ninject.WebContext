using System.Web.Mvc;
using System.Linq;

namespace Ninject.WebContext
{
    /// <summary>
    /// Ninject controller action invoker
    /// </summary>
    public class NinjectControllerActionInvoker : ControllerActionInvoker
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Create a new instance of NinjectControllerActionInvoker
        /// </summary>
        /// <param name="kernel">Ninject kernel</param>
        public NinjectControllerActionInvoker(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Get filters
        /// </summary>
        /// <param name="controllerContext">Controller context</param>
        /// <param name="actionDescriptor">Action descriptor</param>
        /// <returns>FilterInfo</returns>
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var info = base.GetFilters(controllerContext, actionDescriptor);

            info.ActionFilters.ToList().ForEach(Inject);
            info.AuthenticationFilters.ToList().ForEach(Inject);
            info.AuthorizationFilters.ToList().ForEach(Inject);
            info.ExceptionFilters.ToList().ForEach(Inject);
            info.ResultFilters.ToList().ForEach(Inject);

            return info;
        }

        /// <summary>
        /// Inject dependencies into a filter
        /// </summary>
        /// <param name="obj"></param>
        private void Inject(object filter)
        {
            _kernel.Inject(filter);
        }
    }
}
