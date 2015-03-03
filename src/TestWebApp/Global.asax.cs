using System.Web.Mvc;
using System.Web.Routing;
using Ninject.WebContext;
using Ninject.Modules;

namespace TestWebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });

		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			NinjectContext.Instance.AddModule<TestModule>().UseMvc().UseWebApi().Initialize();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}

	public class TestModule : AutoInjectionModule
	{
		public override void Load()
		{
			Bind<ITestService>().To<TestService>().InHttpScope();
		}
	}
}
