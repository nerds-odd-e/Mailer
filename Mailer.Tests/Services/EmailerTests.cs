using System.Collections.Generic;
using Mailer.Services;
using NSubstitute;
using NUnit.Framework;

namespace Mailer.Tests.Services
{
    [TestFixture]
    public class EmailerTests
    {
        private ISmtpClient _smtpClient;
        private Emailer _emailer;

        [SetUp]
        public void SetUp()
        {
            _smtpClient = Substitute.For<ISmtpClient>();
            _emailer = new Emailer(_smtpClient);
        }

        [Test]
        public void DoNotSend_WhenNoRecipient()
        {
            var recipientList = new List<string>();
            var actualEmails = _emailer.ConstructEmails(recipientList);
            Assert.AreEqual(0, actualEmails.Count);
        }

        [Test]
        public void SendOneEmail_WhenOnlyOneRecipient()
        {
            var recipientList = new List<string>{ "test1@gmail.com" };
            var actualEmails = _emailer.ConstructEmails(recipientList);
            Assert.AreEqual(1, actualEmails.Count);
            Assert.AreEqual("test1@gmail.com", actualEmails[0].To[0].Address);
        }

        [Test]
        public void SendMultiple_WhenMultipleRecipient()
        {
            var recipientList = new List<string> {"test1@gmail.com", "test2@gmail.com"};
            var actualEmails = _emailer.ConstructEmails(recipientList);
            Assert.AreEqual(2, actualEmails.Count);
            Assert.AreEqual("test1@gmail.com", actualEmails[0].To[0].Address);
            Assert.AreEqual("test2@gmail.com", actualEmails[1].To[0].Address);
        }

       /* [Test]
        public void SendWithFirstNameLastName()
        {

        }*/
    }


}