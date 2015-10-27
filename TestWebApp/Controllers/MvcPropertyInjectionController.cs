using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApp.Models;
using TestWebApp.Services;

namespace TestWebApp.Controllers
{
    public class MvcPropertyInjectionController : Controller
    {
        public ITestService TestService { get; set; }


        [HttpGet]
        public ActionResult Index()
        {
            return Json(new TestModel
            {
                Value = TestService.HelloWorld()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}