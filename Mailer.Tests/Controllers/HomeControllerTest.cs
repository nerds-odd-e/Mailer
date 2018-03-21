using System.Web.Mvc;
using Mailer.Controllers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Mailer.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SendMail()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.SendAllMail() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
