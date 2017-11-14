using ICBA.Data.Models;
using ICBA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICBA.Web.Controllers
{
    public class HomeController : Controller
    {
        //[OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Duration = 10, VaryByParam = "none")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}