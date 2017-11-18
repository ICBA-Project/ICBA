using ICBA.Data;
using ICBA.Data.Models;
using ICBA.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack;
using TestStack.FluentMVCTesting;

namespace ICBA.UnitTests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AdminPanel_Should
    {
        [Test]
        public void AdminPanel_ReturnDefaultView()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<ApplicationUser>>();

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "Username1"  },
                new ApplicationUser() { UserName = "Username2"   }
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);
            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            AdminController adminController = new AdminController(dbContextMock.Object);

            //  Act & Assert
            adminController.WithCallTo(c => c.AdminPanel())
                .ShouldRenderDefaultView()
                .WithModel<List<ApplicationUser>>(actual =>
                {
                    Assert.IsNotNull(actual);
                    for (int i = 0; i < users.Count(); i++)
                    {
                        Assert.AreEqual(users[i].UserName, actual[i].UserName);
                    }
                });
        }

        [Test]
        public void AdminPanel_ReturnViewWithCorrectParameter()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<ApplicationUser>>();

            //  Act
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "Username1"  },
                new ApplicationUser() { UserName = "Username2"  }
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);
            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            AdminController adminController = new AdminController(dbContextMock.Object);

            //  Assert
            adminController.WithCallTo(c => c.AdminPanel()).ShouldRenderView("AdminPanel")
                .WithModel<List<ApplicationUser>>(actual =>
                {
                    Assert.IsNotNull(actual);
                    for (int i = 0; i < actual.Count; i++)
                    {
                        ApplicationUser user = actual[i];
                        Assert.AreEqual(user.UserName, users[i].UserName);
                    }
                });
        }

        [Test]
        public void EditUser_ReturnViewWithCorrectParameter()
        {
            // Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<ApplicationUser>>();

            //  Act
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "Username1"  },
                new ApplicationUser() { UserName = "Username2"  }
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);
            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            AdminController adminController = new AdminController(dbContextMock.Object);

            //  Assert
            adminController.WithCallTo(c => c.EditUser()).ShouldRenderView("EditUser")
                .WithModel<List<ApplicationUser>>(actual =>
                {
                    Assert.IsNotNull(actual);
                    for (int i = 0; i < actual.Count; i++)
                    {
                        ApplicationUser user = actual[i];
                        Assert.AreEqual(user.UserName, users[i].UserName);
                    }
                });
        }

        //[Test]
        //public void CorrectlyChangeAdminRole_WhenHeIsNotAdmin()
        //{
        //    var dbContextMock = new Mock<ApplicationDbContext>();
        //    var userDbSetMock = new Mock<DbSet<ApplicationUser>>();
        //    var applicationUserAsArgument = new ApplicationUser()
        //    {
        //        UserName = "Username1"
        //    }
    }
}