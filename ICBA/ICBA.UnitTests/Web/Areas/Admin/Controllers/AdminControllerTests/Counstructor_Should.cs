using ICBA.Data;
using ICBA.Services;
using ICBA.Web.Areas.Admin.Controllers;
using ICBA.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBA.UnitTests.Areas.Admin.Controllers.AdminControllerTests
{
    class Counstructor_Should
    {
        [Test]
        public void ReturnInstance_WhenParameterIsCorrect()
        {
            // Arrange
            Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();

            // Act
            AdminController instance = new AdminController(mockApplicationDbContext.Object);

            // Assert
            Assert.IsNotNull(instance);
        }

        [Test]
        public void ThrowArgumentNullException_WhenParametersAreNotCorrect()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new AdminController(null));
        }
    }
}
