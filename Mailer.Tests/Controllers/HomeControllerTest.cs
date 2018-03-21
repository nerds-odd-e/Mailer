using System.Collections.Generic;
using System.Net.Mail;
using Mailer.Controllers;
using Mailer.Services;
using NSubstitute;
using NUnit.Framework;

namespace Mailer.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void SendEmail()
        {
            var fakeClient = Substitute.For<ISmtpClient>();
            var homeController = new HomeController();
            var recipientList = new List<string> { "test1@gmail.com", "test2@gmail.com" };
            homeController.SendEmail(recipientList, fakeClient);
            fakeClient.Received(1).EnableSsl();
            fakeClient.Received(2).Send(Arg.Any<MailMessage>());
        }

        private static int AnyInt()
        {
            return Arg.Any<int>();
        }

        private static string AnyString()
        {
            return Arg.Any<string>();
        }
    }
}
