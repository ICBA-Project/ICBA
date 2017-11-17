//using ICBA.Data;
//using ICBA.Data.Models;
//using ICBA.Services;
//using ICBA.Web.Controllers;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TestStack.FluentMVCTesting;

//namespace ICBA.UnitTests.Controllers.SensorControllerTests
//{
//    [TestFixture]
//    public class OwnSensors
//    {
//        [Test]
//        public void ReturnInstance_WhenParameterIsCorrect()
//        {
//            // Arrange
//            Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();

//            SlackService slackService = new SlackService();
//            Mock<SensorsService> mockSensorService = new Mock<SensorsService>(mockApplicationDbContext.Object, slackService);

//            string str = "userID";

//            // Act
//            Sensor sensor1 = new Sensor() { OwnerId = "userId" };
//            Sensor sensor2 = new Sensor() { Id = new Guid("61FF0614-64FD-4842-9A05-0B1541D2CC61") };

//            List<Sensor> sensorsInDbMock = new List<Sensor> { sensor1, sensor2 };
//            var sensorsInDb = new Mock<DbSet<Sensor>>().SetupData(sensorsInDbMock);
//            mockApplicationDbContext.SetupGet(s => s.Sensors).Returns(sensorsInDb.Object);

//            SensorController sensorController = new SensorController(mockSensorService.Object, mockApplicationDbContext.Object, slackService);
//            //Assert
//            sensorController.WithCallTo(s => s.OwnSensors()).ShouldRenderView("SensorsDisplay")

//                .WithModel<List<Sensor>>(actual =>
//                {
//                    Assert.IsNotNull(actual);
//                    for (int i = 0; i < actual.Count; i++)
//                    {
//                        Sensor sensor = actual[i];
//                        Assert.AreEqual(sensorsInDb.Object.Where(s => s.Id == sensor.Id).Select(s => s.AccessIsPublic).FirstOrDefault(), sensor.AccessIsPublic);
//                    }
//                });
//        }
//    }
//}
