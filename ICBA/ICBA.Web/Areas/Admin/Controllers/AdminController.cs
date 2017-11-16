using Bytes2you.Validation;
using ICBA.Data;
using ICBA.Data.Models;
using ICBA.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public ActionResult AdminPanel()
        {
            IEnumerable<ApplicationUser> usersInDb = new List<ApplicationUser>();

            usersInDb = dbContext.Users.ToList();
            return View("AdminPanel", usersInDb);
        }

        public ActionResult EditUser()
        {
            IEnumerable<ApplicationUser> usersinDb = dbContext.Users.ToList();
            return View("EditUser", usersinDb);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(ApplicationUser user)
        {
            // I'm sorry, Vik, but it's Wed 15 Nov 21:28 right now. Don't have enough time to decouple right now! :(
            ApplicationDbContext userscontext = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(userscontext);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var dbUser = this.dbContext.Users.First(u => u.UserName == user.UserName);

            if (user.Email == "on")
            {
                if (!userManager.IsInRole(dbUser.Id, "Admin"))
                {
                    userManager.AddToRole(dbUser.Id, "Admin");
                }
                this.dbContext.SaveChanges();
            } else {
                if (userManager.IsInRole(dbUser.Id, "Admin"))
                {
                    userManager.RemoveFromRole(dbUser.Id, "Admin");
                }
                this.dbContext.SaveChanges();
            }

            return this.View("EditedUser");
        }
    }
}