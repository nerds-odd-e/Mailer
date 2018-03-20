using System.Collections.Generic;
using NUnit.Framework;
using System.Net.Mail;
using Mailer.Controllers;
using Mailer.Services;
using NSubstitute;

namespace Mailer.Tests
{
    [TestFixture]
    public class EmailTest
    {
        [Test]
        public void SendEmail()
        {
            var fakeClient = Substitute.For<ISmtpClient>();
            var homeController = new HomeController();
            homeController.Client = fakeClient;
            fakeClient.Received(1).Initialize(AnyString(), AnyInt(), AnyString(), AnyString());
            var recipientList = new List<string>{"test1@gmail.com","test2@gmail.com"};
            homeController.SendEmail(recipientList);
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