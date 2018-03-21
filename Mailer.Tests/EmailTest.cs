using System;
using NUnit.Framework;
using System.Net.Mail;
using dotenv.net;
using Mailer.Services;
using netDumbster.smtp;

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
            DotEnv.Config(true, envPath + ".env");
        }

        [Test]
        public void SendEmailToLocalSmtpServer()
        {
            var host= Environment.GetEnvironmentVariable("smtpServer");
            var port = int.Parse(Environment.GetEnvironmentVariable("smtpPort") ?? "25");
            var email = Environment.GetEnvironmentVariable("senderEmail");
            var password = Environment.GetEnvironmentVariable("senderPassword");
            var server = SimpleSmtpServer.Start(25);
            var client = new SmtpClientWrapper();
            client.Initialize(host, port, email, password);
            var mail = new MailMessage("from@gmail.com", "to@gmail.com");
            client.Send(mail);
            Assert.AreEqual(1, server.ReceivedEmailCount);
            var smtpMessage = server.ReceivedEmail[0];
            Assert.AreEqual("from@gmail.com", smtpMessage.FromAddress.Address);
            Assert.AreEqual("to@gmail.com", smtpMessage.ToAddresses[0].Address);
            client.SmtpClient.Dispose();
            server.Stop();
        }

        
    }

   
}