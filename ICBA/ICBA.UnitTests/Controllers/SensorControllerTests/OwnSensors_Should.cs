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

            string userId = "userID";
            Sensor sensor1 = new Sensor() { OwnerId = userId };
            // Act

            List<Sensor> sensorsInDbMock = new List<Sensor> { sensor1 };
            List<ApplicationUser> usersInDbMock = new List<ApplicationUser> { user };

            var sensorsInDb = new Mock<DbSet<Sensor>>().SetupData(sensorsInDbMock);
            var usersInDb = new Mock<DbSet<ApplicationUser>>().SetupData(usersInDbMock);

            mockApplicationDbContext.SetupGet(s => s.Users).Returns(usersInDb.Object);
            mockApplicationDbContext.SetupGet(s => s.Sensors).Returns(sensorsInDb.Object);
          

            SensorController sensorController = new SensorController(mockSensorService.Object, mockApplicationDbContext.Object, slackService);

            var user = sensorController.User;
            user.Identity. = userId;

            //Assert
            sensorController.WithCallTo(s => s.OwnSensors()).ShouldRenderView("SensorsDisplay")

                .WithModel<List<Sensor>>(actual =>
                {
                    Assert.IsNotNull(actual);
                    CollectionAssert.AreEqual(sensorsInDb.Object.First().OwnerId, actual.First().OwnerId);
                });
        }
    }
}
