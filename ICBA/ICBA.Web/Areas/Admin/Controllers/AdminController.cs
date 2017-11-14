using ICBA.Data;
using ICBA.Data.Models;
using ICBA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICBA.Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private SensorsService sensorsService;
        private ApplicationDbContext dbContext;

        public AdminController(SensorsService sensorsService, ApplicationDbContext dbContext)
        {
            this.sensorsService = sensorsService;
            this.dbContext = dbContext;
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            IEnumerable<ApplicationUser> usersInDb = new List<ApplicationUser>();
            IEnumerable<Sensor> sensorsInDb = new List<Sensor>();

            usersInDb = dbContext.Users.ToList();

            return View("AdminPanel", usersInDb);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            IEnumerable<ApplicationUser> usersInDb = dbContext.Users.ToList();

            return View();
        }
    }
}