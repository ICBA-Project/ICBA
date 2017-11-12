using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICBA.Services;

namespace ICBA.Web.Controllers
{
    public class SensorController : Controller
    {
        private SensorsService sensorsService;

        public SensorController(SensorsService sensorsService)
        {
            this.sensorsService = sensorsService;
        }

        public ActionResult WinService()
        {
            if (this.HttpContext.Request.Headers["auth-token"] == "0b3fb7f14591-cf7b-2834-d1e5-ef64c4e8")
            {
                sensorsService.WinService();
            }
            return View();
        }
    }
}