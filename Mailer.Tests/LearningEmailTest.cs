using System;
using dotenv.net;
using Mailer.Services;
using netDumbster.smtp;
using NUnit.Framework;

namespace Mailer.Tests
{
    [TestFixture]
    class LearningEmailTest
    {
        private SimpleSmtpServer _simpleSmtpServer;
        private SmtpMailer _smtpMailer;
        0
        [SetUp]
        public void SetUp()
        {
            _simpleSmtpServer = SimpleSmtpServer.Start(25);
            Environment.SetEnvironmentVariable("Host", "localhost");
            Environment.SetEnvironmentVariable("Port", "25");
            _smtpMailer = new SmtpMailer();
        }

        [TearDown]
        public void TDown()
        {
            _smtpMailer.Dispose();
            _simpleSmtpServer.Stop();
            _simpleSmtpServer.Dispose();
        }

        [Test]
        public void Test1()
        {
            var expectedBodyMesage = "test body message";
            _smtpMailer.SendOneEmail(expectedBodyMesage, "to@gmail.com");
            Assert.AreEqual(1, _simpleSmtpServer.ReceivedEmailCount);
            Assert.AreEqual(expectedBodyMesage, _simpleSmtpServer.ReceivedEmail[0].MessageParts[0].BodyData);
        }
    }
}
