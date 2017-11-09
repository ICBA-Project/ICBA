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
        private readonly ISensorsService sensorsService;

        public HomeController(ISensorsService sensorsService)
        {
            this.sensorsService = sensorsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ICollection<Sensor> sensors = sensorsService.ReadSensorsAll();
            return View(sensors);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}