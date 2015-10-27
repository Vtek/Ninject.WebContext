using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using TestWebApp.Services;

namespace TestWebApp
{
    public class TestWebApiFilter : ActionFilterAttribute
    {
        public ITestService TestService { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            TestService.HelloWorld();
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}