using ICBA.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ICBA.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ICBA.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ICBA.Data.ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole() { Name = "Admin" });
                context.SaveChanges();
            }
            if (context.Users.Select(m => m.Roles.Count > 0).Count() == 0)
            {
                PasswordHasher passwordHasher = new PasswordHasher();
                string passwordHash = passwordHasher.HashPassword("ICBA-DB");
                ApplicationUser user = new ApplicationUser
                {
                    Email = "ICBA-DB@db.owner",
                    UserName = "ICBA-DB",
                    PasswordHash = passwordHash,
                    SecurityStamp = Guid.NewGuid().ToString()

                };
                context.Users.Add(user);
                IdentityRole role = context.Roles.Where(m => m.Name == "Admin").Single();
                user.Roles.Add(new IdentityUserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
                context.SaveChanges();
            }
        }
    }
}
