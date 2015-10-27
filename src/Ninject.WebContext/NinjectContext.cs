using System;
using System.Linq;
using Ninject.Modules;
using System.Collections.Generic;
using Ninject.Selection.Heuristics;
using System.Web.Mvc;
using System.Web.Http;

namespace Ninject.WebContext
{
    /// <summary>
    /// Ninject context.
    /// </summary>
    public class NinjectContext
	{
        /// <summary>
        /// The synchronize root object.
        /// </summary>
        static readonly object SyncRoot = new object();

        /// <summary>
        /// Single instance of the NinjectContext
        /// </summary>
        static readonly NinjectContext _instance = new NinjectContext();

        /// <summary>
		/// Ninject module to use in the context.
		/// </summary>
        readonly IList<INinjectModule> _modules = new List<INinjectModule>();

		/// <summary>
		/// True if NinjectContext was initialized, otherwise false
		/// </summary>
		static bool _initialized;

		/// <summary>
		/// True if Mvc is use in the WebApplication, otherwise false.
		/// </summary>
		bool _useMvc;

        bool _withAutoInjection;

        /// <summary>
        /// True if WebApi is use in the WebApplication, otherwise false.
        /// </summary>
        bool _useWebApi;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static NinjectContext Get()
		{
				lock (SyncRoot)
					return _instance;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		/// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
		public bool Initialized
		{
			get
			{
				lock (SyncRoot)
					return _initialized;
			}
		}

		/// <summary>
		/// Gets the modules.
		/// </summary>
		/// <value>The modules.</value>
		public IList<INinjectModule> Modules
		{
			get 
			{
				lock (SyncRoot)
					return _modules;
			}
		}

		/// <summary>
		/// Gets the inject types.
		/// </summary>
		/// <value>The inject types.</value>
		public IList<Type> InjectTypes
		{
			get 
			{
				lock (SyncRoot)
					return AutoInjection.ShouldInjectPropertyTypes;
			}
		}

		/// <summary>
		/// Adds the module.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public NinjectContext AddModule<T>() where T : class, INinjectModule
		{
			lock (SyncRoot)
			{
				if (Initialized) return this;

				var module = (T)Activator.CreateInstance(typeof(T));

				if (_modules.Any(x => x.GetType().Equals(module.GetType())))
					return this;

				_modules.Add(module);
				return this;
			}
		}

		/// <summary>
		/// Uses MVC.
		/// </summary>
		public NinjectContext UseMvc()
		{
			lock(SyncRoot)
			{
                if (Initialized) return this;

                _useMvc = true;
				return this;
			}
		}

		/// <summary>
		/// Use WebApi.
		/// </summary>
		public NinjectContext UseWebApi()
		{
			lock(SyncRoot)
			{
                if (Initialized) return this;

                _useWebApi = true;
				return this;
			}
		}

        public NinjectContext WithAutoInjection()
        {
            lock (SyncRoot)
            {
                if (Initialized) return this;

                _withAutoInjection = true;
                return this;
            }
        }

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize()
		{
			lock (SyncRoot)
			{
                if (Initialized) return;

                var kernel = new StandardKernel(_modules.ToArray());
                if (_withAutoInjection)
                    kernel.Components.Add<IInjectionHeuristic, AutoInjection>();
                
                var ninjectDependencyResolver = new NinjectDependencyResolver(kernel);

                if (_useMvc)
                {
                    kernel.Bind<IActionInvoker>().To<NinjectControllerActionInvoker>().InTransientScope();
                    DependencyResolver.SetResolver(ninjectDependencyResolver);
                }   

                if(_useWebApi)
                {
                    GlobalConfiguration.Configuration.DependencyResolver = ninjectDependencyResolver;
                    GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Filters.IFilterProvider), new NinjectFilterProvider(kernel));
                }
                    

                _initialized = true;
			}
		}
	}
}

