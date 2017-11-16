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

namespace ICBA.UnitTests.Controllers.SensorControllerTests
{
    [TestFixture]
    public class PublicSensors_Should
    {
        [Test]
        public void ReturnInstance_WhenParameterIsCorrect()
        {
            // Arrange
            Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();

            Mock<SensorsService> mockSensorService = new Mock<SensorsService>(mockApplicationDbContext.Object);

            // Act
            Sensor sensor1 = new Sensor() { Id = "alli"};
            Sensor sensor2 = new Sensor();

            List<Sensor> sensorsInDbMock = new List<Sensor> { sensor1, sensor2 };
            var sensorsInDb = new Mock<DbSet<Sensor>>().SetupData(sensorsInDbMock);
            mockApplicationDbContext.SetupGet(s => s.Sensors).Returns(sensorsInDb.Object);

            SensorController sensorController = new SensorController(mockSensorService.Object, mockApplicationDbContext.Object);

            //Assert
            sensorController.WithCallTo(s => s.PublicSensors("alli")).ShouldRenderView("SensorsDisplay")

                .WithModel<List<Sensor>>(actual =>
                {
                    Assert.IsNotNull(actual);
                    CollectionAssert.AreEquivalent(sensorsInDb.Object, actual);
                });
        }
    }
}
