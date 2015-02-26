using System;
using System.Linq;
using Ninject.Modules;
using System.Collections.Generic;
using Ninject.Selection.Heuristics;
using System.IO;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Ninject.WebContext
{
	/// <summary>
	/// Ninject context.
	/// </summary>
	public class NinjectContext
	{
		/// <summary>
		/// Initializes a new instance of the NinjectContext class.
		/// </summary>
		NinjectContext()
		{
			_modules = new List<INinjectModule>();
		}

		/// <summary>
		/// The instance.
		/// </summary>
		static NinjectContext _instance;

		/// <summary>
		/// The is initialized.
		/// </summary>
		bool _isInitialized;

		/// <summary>
		/// The use mvc.
		/// </summary>
		bool _useMvc;

		/// <summary>
		/// The use web API.
		/// </summary>
		bool _useWebApi;

		/// <summary>
		/// The kernel.
		/// </summary>
		IKernel _kernel;

		/// <summary>
		/// The modules.
		/// </summary>
		readonly IList<INinjectModule> _modules;

		/// <summary>
		/// The synchronize root object.
		/// </summary>
		static readonly object SyncRoot = new object();

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static NinjectContext Instance
		{
			get
			{
				lock (SyncRoot)
					return _instance ?? (_instance = new NinjectContext());
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		/// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
		public bool IsInitialized
		{
			get
			{
				lock (SyncRoot)
					return _isInitialized;
			}
		}

		/// <summary>
		/// Gets the kernel.
		/// </summary>
		/// <value>The kernel.</value>
		public IKernel Kernel
		{
			get
			{
				lock (SyncRoot)
					return _kernel;
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
				if (_isInitialized) return this;

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
				_useWebApi = true;
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
				if (_isInitialized) return;

				_kernel = new StandardKernel(_modules.ToArray());
				_kernel.Components.Remove<IInjectionHeuristic, StandardInjectionHeuristic>();
				_kernel.Components.Add<IInjectionHeuristic, AutoInjection>();

				if(_useMvc)
					ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

				if(_useWebApi)
					GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new NinjectControllerActivator());

				_isInitialized = true;
			}
		}

		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset()
		{
			lock (SyncRoot)
			{
				AutoInjection.ShouldInjectPropertyTypes.Clear();
				_instance = null;
				_modules.Clear();
				_kernel.Dispose();
				_isInitialized = false;
			}

		}
	}
}

