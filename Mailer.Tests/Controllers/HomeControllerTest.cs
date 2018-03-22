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
        private ISmtpClient fakeClient;
        private HomeController homeController;
        [SetUp]
        public void Init()
        {
            fakeClient = Substitute.For<ISmtpClient>();
            homeController = new HomeController
            {
                SmtpClient = fakeClient
            };
        }
        [Test]
        public void SendEmail()
        {
            var recipientList = new List<string> { "test1@gmail.com", "test2@gmail.com" };
            homeController.SendEmail(recipientList, fakeClient);
            fakeClient.Received(1).EnableSsl();
            fakeClient.Received(2).Send(Arg.Any<MailMessage>());
        }
    }
}
