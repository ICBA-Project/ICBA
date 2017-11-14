using Bytes2you.Validation;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext dbContext;
        public AdminController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
        }

        public ActionResult AdminPanel()
        {
            IEnumerable<ApplicationUser> usersInDb = new List<ApplicationUser>();

            usersInDb = dbContext.Users.ToList();
            return View("AdminPanel", usersInDb);
        }

        public ActionResult EditUser()
        {
            return View();
        }

    }
}