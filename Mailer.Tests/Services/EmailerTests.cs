using System.Collections.Generic;
using Mailer.Services;
using NSubstitute;
using NUnit.Framework;

namespace Mailer.Tests.Services
{
    [TestFixture]
    public class EmailerTests
    {
        [Test]
        public void TestEmailer()
        {
            var client = Substitute.For<ISmtpClient>();
            var emailer = new Emailer(client);
            var recipientList = new List<string> { "test1@gmail.com", "test2@gmail.com" };
            var subject = "this is a test email.";
            var body = "this is my test email body";
            var fromAddress = "myodde@gmail.com";

            var actualEmails = emailer.ConstructEmails(recipientList);
            Assert.AreEqual(2, actualEmails.Count);
            Assert.IsTrue(actualEmails[0].From.Address.Contains(fromAddress));
            Assert.AreEqual("test1@gmail.com", actualEmails[0].To[0].Address);
            Assert.AreEqual(subject, actualEmails[0].Subject);
            Assert.AreEqual(body, actualEmails[0].Body);

            Assert.IsTrue(actualEmails[1].From.Address.Contains(fromAddress));
            Assert.AreEqual("test2@gmail.com", actualEmails[1].To[0].Address);
            Assert.AreEqual(subject, actualEmails[1].Subject);
            Assert.AreEqual(body, actualEmails[1].Body);
        }
    }


}