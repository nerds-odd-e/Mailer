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
            MailMessage mail = new MailMessage("myodde@gmail.com", "shelvia.ho@gmail.com");
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            homeController.SendEmail(mail);
            fakeClient.Received(1).Send(Arg.Any<MailMessage>());
        }
    }

   
}