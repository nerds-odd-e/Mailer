using System.Collections.Generic;
using Mailer.Models;
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
        private List<Contact> _recipientList;
        private List<Contact> _emptyRecipientList;
        private Contact contact1;
        private Contact contact2;

        [SetUp]
        public void SetUp()
        {
            contact1 = new Contact()
            {
                FirstName = "First_1",
                LastName = "Last_1",
                Email = "test1@gmail.com"
            };
            contact2 = new Contact()
            {
                FirstName = "First_2",
                LastName = "Last_2",
                Email = "test2@gmail.com"
            };
            _smtpClient = Substitute.For<ISmtpClient>();
            _emailer = new Emailer(_smtpClient);
            _recipientList = new List<Contact>();
            _emptyRecipientList = new List<Contact>();
        }

        [Test]
        public void DoNotSend_WhenNoRecipient()
        {
            var actualEmails = _emailer.ConstructPersonalizedEmails(_emptyRecipientList);
            Assert.AreEqual(0, actualEmails.Count);
        }

        [Test]
        public void SendOneEmail_WhenOnlyOneRecipient()
        {
            _recipientList.Add(contact1);
            var actualEmails = _emailer.ConstructPersonalizedEmails(_recipientList);
            Assert.AreEqual(1, actualEmails.Count);
            Assert.AreEqual(contact1.Email, actualEmails[0].To[0].Address);
            Assert.IsTrue(actualEmails[0].Body.StartsWith("Hi " + contact1.FirstName + " " + contact1.LastName));
        }

        [Test]
        public void SendMultiple_WhenMultipleRecipient()
        {
            _recipientList.Add(contact1);
            _recipientList.Add(contact2);
            var actualEmails = _emailer.ConstructPersonalizedEmails(_recipientList);

            Assert.AreEqual(2, actualEmails.Count);
            Assert.AreEqual(contact1.Email, actualEmails[0].To[0].Address);
            Assert.IsTrue(actualEmails[0].Body.StartsWith("Hi " + contact1.FirstName + " " + contact1.LastName));

            Assert.AreEqual(contact2.Email, actualEmails[1].To[0].Address);
            Assert.IsTrue(actualEmails[1].Body.StartsWith("Hi " + contact2.FirstName + " " + contact2.LastName));
        }
        
        [Test]
        public void SendWithFirstNameAndNoLastName()
        {
            var contacts = new List<Contact>() { new Contact() { FirstName = "First", Email = "test@gmail.com" } };

            var emailMessages = _emailer.ConstructPersonalizedEmails(contacts);
            Assert.AreEqual(1, emailMessages.Count);
            Assert.IsTrue(emailMessages[0].Body.StartsWith("Hi First"));
        }
    }


}