using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
		public ITestService TestService { get; set; }

        public ActionResult Index()
        {
			return View (new Index() { Value = TestService.Hello() });
        }
    }
}
