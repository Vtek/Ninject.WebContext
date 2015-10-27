using System.Web.Mvc;
using TestWebApp.Services;

namespace TestWebApp
{
    public class TestMvcFilter : ActionFilterAttribute
    {
        public ITestService TestService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TestService.HelloWorld();
            base.OnActionExecuted(filterContext);
        }
    }
}