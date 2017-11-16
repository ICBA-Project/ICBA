using Bytes2you.Validation;
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
        private SlackService slackService;

        public HomeController(SlackService slackService)
        {
            Guard.WhenArgument(slackService, "slackService").IsNull().Throw();

            this.slackService = slackService;
        }

        //[OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Index()
        {
            slackService.PostMessage("User " + this.User.Identity.Name + " requested " + this.Request.Url + " url @" + this.HttpContext.Timestamp + ".");
            return View();
        }

        [OutputCache(Duration = 30)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}