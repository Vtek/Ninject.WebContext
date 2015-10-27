using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Services
{
    public class TestService : ITestService
    {
        public string HelloWorld()
        {
            return "Hello world !";
        }
    }
}