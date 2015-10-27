using Ninject.Modules;
using Ninject.WebContext;
using TestWebApp.Services;

namespace TestWebApp
{
    public class TestModule : AutoInjectionModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestService>().InHttpScope();
        }
    }
}