using Ninject.Syntax;
using System;
using System.Collections.Generic;

namespace Ninject.WebContext.Resolver
{
    /// <summary>
    /// Ninject dependency resolver
    /// </summary>
    public sealed class NinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyScope, System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IKernel kernel)
            :this((IResolutionRoot)kernel)
        {
            _kernel = kernel;
        }

        public NinjectDependencyResolver(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new NinjectDependencyResolver(_kernel.BeginBlock());
        }

        public void Dispose()
        {
            
        }

        public object GetService(Type serviceType)
        {
           return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}
