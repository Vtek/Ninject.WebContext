using Ninject.Syntax;
using System;
using System.Collections.Generic;

namespace Ninject.WebContext
{
    /// <summary>
    /// Ninject dependency resolver
    /// </summary>
    public sealed class NinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyScope, System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;
        private readonly IResolutionRoot _resolutionRoot;

        /// <summary>
        /// Create a new instance of NinjectDependencyResolver (use for MVC)
        /// </summary>
        /// <param name="kernel"></param>
        public NinjectDependencyResolver(IKernel kernel)
            :this((IResolutionRoot)kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Create a new instance of NinjectDepencyResolver (use for WebApi)
        /// </summary>
        /// <param name="resolutionRoot"></param>
        public NinjectDependencyResolver(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        /// <summary>
        /// Begin Scope (WebApi)
        /// </summary>
        /// <returns></returns>
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new NinjectDependencyResolver(_kernel.BeginBlock());
        }

        /// <summary>
        /// Dispose the NinjectDependencyResolver
        /// </summary>
        public void Dispose()
        {
            //we have nothing to dispose
        }

        /// <summary>
        /// Get Service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Instance of service type</returns>
        public object GetService(Type serviceType)
        {
           return _resolutionRoot.TryGet(serviceType);
        }

        /// <summary>
        /// Get all Service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>List of Instance of service type</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}
