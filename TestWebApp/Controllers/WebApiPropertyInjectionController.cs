using System.Web.Http;
using TestWebApp.Models;
using TestWebApp.Services;

namespace TestWebApp.Controllers
{
    public class WebApiPropertyInjectionController : ApiController
    {
        public ITestService TestService { get; set; }

        public WebApiPropertyInjectionController(ITestService testService)
        {
            TestService = testService;
        }

        [TestWebApiFilter]
        public TestModel Get()
        {
            return new TestModel
            {
                Value = TestService.HelloWorld()
            };
        }
    }
}
