using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICBA.Web.Controllers
{
    public class TemperController : Controller
    {
        // GET: Temper
        public ActionResult SensorsTable()
        {
            return View();
        }
    }
}