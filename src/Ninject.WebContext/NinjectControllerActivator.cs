using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Ninject.WebContext
{
	public class NinjectControllerActivator : IHttpControllerActivator
	{
		/// <summary>
		/// Gets or sets the activator.
		/// </summary>
		/// <value>The activator.</value>
		DefaultHttpControllerActivator Activator { get; set; }

		/// <summary>
		/// Initializes a new instance of the NinjectControllerActivator class.
		/// </summary>
		public NinjectControllerActivator()
		{
			Activator = new DefaultHttpControllerActivator();
		}

		/// <summary>
		/// Create the controller instance.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="controllerDescriptor">Controller descriptor.</param>
		/// <param name="controllerType">Controller type.</param>
		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			var controller = Activator.Create(request, controllerDescriptor, controllerType);

			if (controller != null)
				NinjectContext.Instance.Kernel.Inject(controller);

			request.RegisterForDispose(
				new Release(() =>
				{
					if (controller != null)
						NinjectContext.Instance.Kernel.Release(controller);
				}));

			return controller;
		}
	}
}

