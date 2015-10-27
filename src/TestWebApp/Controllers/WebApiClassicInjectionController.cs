using System.Web.Http;
using TestWebApp.Models;
using TestWebApp.Services;

namespace TestWebApp.Controllers
{
    public class WebApiClassicInjectionController : ApiController
    {
        private ITestService TestService { get; set; }

        public WebApiClassicInjectionController(ITestService testService)
        {
            TestService = testService;
        }

        public TestModel Get()
        {
            return new TestModel
            {
                Value = TestService.HelloWorld()
            };
        }
    }
}
