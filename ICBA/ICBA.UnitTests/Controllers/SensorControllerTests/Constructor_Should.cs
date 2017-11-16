//using ICBA.Data;
//using ICBA.Services;
//using ICBA.Web.Controllers;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace ICBA.UnitTests.Controllers.SensorControllerTests
//{
//    [TestFixture]
//    public class Constructor_Should
//    {
//        [Test]
//        public void ReturnInstance_WhenParameterIsCorrect()
//        {
//            // Arrange
//            Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();
//            SlackService slackService = new SlackService();
//            Mock<SensorsService> mockSensorService = new Mock<SensorsService>(mockApplicationDbContext.Object);

//            // Act
//            SensorController instance = new SensorController(mockSensorService.Object, mockApplicationDbContext.Object, slackService);

//            // Assert
//            Assert.IsNotNull(instance);
//        }

//        [Test]
//        public void ThrowArgumentNullException_WhenParametersAreNotCorrect()
//        {
//            // Arrange
//            Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();
//            Mock<SensorsService> mockSensorApiService = new Mock<SensorsService>(mockApplicationDbContext.Object);
//            SlackService slackService = new SlackService();
//            // Act
//            // Assert
//            Assert.Throws<ArgumentNullException>(() => new SensorController(null, mockApplicationDbContext.Object,slackService));
//            Assert.Throws<ArgumentNullException>(() => new SensorController(mockSensorApiService.Object, null, slackService));
//        }
//    }
//}
