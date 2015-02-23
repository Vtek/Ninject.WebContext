using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.WebContext
{
	/// <summary>
	/// Ninject controller factory.
	/// </summary>
	public class NinjectControllerFactory : DefaultControllerFactory
	{
		/// <summary>
		/// Gets the controller instance.
		/// </summary>
		/// <returns>The controller instance.</returns>
		/// <param name="requestContext">Request context.</param>
		/// <param name="controllerType">Controller type.</param>
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			var controller = base.GetControllerInstance(requestContext, controllerType);

			if (controller != null)
				NinjectContext.Instance.Kernel.Inject(controller);

			return controller;
		}

		/// <summary>
		/// Releases the specified controller.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public override void ReleaseController(IController controller)
		{
			if (controller != null)
				NinjectContext.Instance.Kernel.Release(controller);

			base.ReleaseController(controller);
		}
	}
}

