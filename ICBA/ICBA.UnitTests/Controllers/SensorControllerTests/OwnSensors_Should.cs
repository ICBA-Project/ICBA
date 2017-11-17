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

            string userId = "";
            Sensor sensor1 = new Sensor() { OwnerId = userId };

            // Act

            List<Sensor> sensorsInDbMock = new List<Sensor> { sensor1 };
            var sensorsInDb = new Mock<DbSet<Sensor>>().SetupData(sensorsInDbMock);          
            mockApplicationDbContext.SetupGet(s => s.Sensors).Returns(sensorsInDb.Object);

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity(userId);
            var principal = new GenericPrincipal(fakeIdentity, null);
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            Mock<ControllerContext> mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            SensorController sensorController = new SensorController(mockSensorService.Object, mockApplicationDbContext.Object, slackService);
            
            sensorController.ControllerContext = mockControllerContext.Object;


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
