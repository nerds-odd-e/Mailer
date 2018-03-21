using System;
using NUnit.Framework;
using System.Net.Mail;
using dotenv.net;
using Mailer.Models;
using Mailer.Services;
using netDumbster.smtp;

namespace Mailer.Tests
{
    [TestFixture]
    public class EmailTest
    {
        private SmtpClientWrapper _smtpClientWrapper;
        private SimpleSmtpServer _simpleSmtpServer;

        [SetUp]
        public void Setup()
        {
            var testRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var envPath = testRootPath.Substring(0, testRootPath.IndexOf("bin", StringComparison.Ordinal));
            DotEnv.Config(true, envPath + ".env");

            using (var db = new MailerDbEntities())
            {
                db.Database.ExecuteSqlCommand("delete from Contact");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _smtpClientWrapper.Dispose();
            _simpleSmtpServer.Stop();
        }

        [Test]
        public void SendEmailToLocalSmtpServer()
        {
            _simpleSmtpServer = SimpleSmtpServer.Start(25);
            _smtpClientWrapper = new SmtpClientWrapper();
            var mail = new MailMessage("from@gmail.com", "to@gmail.com");
            _smtpClientWrapper.Send(mail);
            Assert.AreEqual(1, _simpleSmtpServer.ReceivedEmailCount);
            var smtpMessage = _simpleSmtpServer.ReceivedEmail[0];
            Assert.AreEqual("from@gmail.com", smtpMessage.FromAddress.Address);
            Assert.AreEqual("to@gmail.com", smtpMessage.ToAddresses[0].Address);
        }
    }

   
}