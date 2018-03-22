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
        private ISmtpClient _fakeClient;
        private HomeController _homeController;
        [SetUp]
        public void Init()
        {
            _fakeClient = Substitute.For<ISmtpClient>();
            _homeController = new HomeController
            {
                Client = _fakeClient
            };
        }
        [Test]
        public void SendEmail()
        {
            var recipientList = new List<string> { "test1@gmail.com", "test2@gmail.com" };
            _homeController.SendEmail(recipientList, _fakeClient);
            _fakeClient.Received(2).Send(Arg.Any<MailMessage>());
        }
    }
}
