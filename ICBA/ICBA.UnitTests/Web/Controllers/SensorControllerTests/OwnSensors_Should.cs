using ICBA.Data;
using ICBA.Data.Models;
using ICBA.Services;
using ICBA.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;

namespace ICBA.UnitTests.Controllers.SensorControllerTests
{
    [TestFixture]
    public class OwnSensors
    {
        [Test]
        public void ReturnInstance_WhenParameterIsCorrect()
        {
            // Arrange
            Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();

            SlackService slackService = new SlackService();
            Mock<SensorsService> mockSensorService = new Mock<SensorsService>(mockApplicationDbContext.Object, slackService);

            string userName = "test";
            Sensor sensor1 = new Sensor() { OwnerId = userName };
            ApplicationUser user1 = new ApplicationUser() { UserName = userName, Id = "1", SharedWithUserSensors = new List<Sensor>() { sensor1 } };

            List<Sensor> sensorsInDbMock = new List<Sensor> { sensor1 };
            List<ApplicationUser> usersInDbMock = new List<ApplicationUser> { user1 };
            Mock<DbSet<Sensor>> sensorsInDb = new Mock<DbSet<Sensor>>().SetupData(sensorsInDbMock);
            Mock<DbSet<ApplicationUser>> usersInDb = new Mock<DbSet<ApplicationUser>>().SetupData(usersInDbMock);

            var context = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            context.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("test");

            SensorController sensorController = new SensorController(mockSensorService.Object, mockApplicationDbContext.Object, slackService);
            mockApplicationDbContext.SetupGet(s => s.Sensors).Returns(sensorsInDb.Object);
            mockApplicationDbContext.SetupGet(s => s.Users).Returns(usersInDb.Object);
            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(x => x.Identity.Name).Returns("test");

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(x => x.User).Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext)
                                 .Returns(contextMock.Object);

            sensorController.ControllerContext = controllerContextMock.Object;


            //Act & Assert
            //sensorController.WithCallTo(s => s.OwnSensors()).ShouldRenderView("SensorsDisplay")

            //    .WithModel<List<Sensor>>(actual =>
            //    {
            //        Assert.IsNotNull(actual);
            //        CollectionAssert.AreEqual(sensorsInDb.Object.First().OwnerId, actual.First().OwnerId);
            //    });
        }
    }
}
