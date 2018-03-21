using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Net.Mail;
using dotenv.net;
using Mailer.Controllers;
using Mailer.Services;
using netDumbster.smtp;
using NSubstitute;

namespace Mailer.Tests
{
    [TestFixture]
    public class EmailTest
    {
        [SetUp]
        public void Setup()
        {
            var testRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var envPath = testRootPath.Substring(0, testRootPath.IndexOf("bin"));
            DotEnv.Config(true, envPath+".env");
        }

        [Test]
        public void SendEmailToLocalSmtpServer()
        {
            var server = SimpleSmtpServer.Start(25);
            var client = new SmtpClientWrapper();
            client.Initialize("localhost", 25, "", "");
            var mail = new MailMessage("from@gmail.com", "to@gmail.com");
            client.Send(mail);
            Assert.AreEqual(1, server.ReceivedEmailCount);
            var smtpMessage = server.ReceivedEmail[0];
            Assert.AreEqual("from@gmail.com", smtpMessage.FromAddress.Address);
            Assert.AreEqual("to@gmail.com", smtpMessage.ToAddresses[0].Address);
            client.SmtpClient.Dispose();
            server.Stop();
        }

        [Test]
        public void SendEmail()
        {
            var fakeClient = Substitute.For<ISmtpClient>();
            var homeController = new HomeController();
            homeController.Client = fakeClient;
            fakeClient.Received(1).EnableSsl();
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