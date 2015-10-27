using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Ninject.WebContext
{
    /// <summary>
    /// Filter provider
    /// </summary>
    public class NinjectFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Create a new instancce of NinjectFilterProvider
        /// </summary>
        /// <param name="kernel">Ninject Kernel</param>
        public NinjectFilterProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Get Filters
        /// </summary>
        /// <param name="configuration">Http configuration</param>
        /// <param name="actionDescriptor">Action descriptor</param>
        /// <returns></returns>
        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);
            
            if(filters != null)
                foreach(var filter in filters)
                    _kernel.Inject(filter.Instance);

            return filters;
        }
    }
}
