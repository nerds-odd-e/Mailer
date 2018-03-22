using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mailer.Controllers;
using Mailer.Models;
using Mailer.Services;
using netDumbster.smtp;
using NUnit.Framework;

namespace Mailer.Tests
{
    [TestFixture]
    class HomeControllerTest
    {
        private SimpleSmtpServer _simpleSmtpServer;

        [SetUp]
        public void SetUp()
        {
            _simpleSmtpServer = SimpleSmtpServer.Start(25);
            Environment.SetEnvironmentVariable("Host", "localhost");
            Environment.SetEnvironmentVariable("Port", "25");
            new SmtpMailer();
        }
        [TearDown]
        public void TDown()
        {
            _simpleSmtpServer.Stop();
            _simpleSmtpServer.Dispose();
        }
        [Test]
        public void IndexTest()
        {
            var homecontroller = new HomeController();
            var result = homecontroller.Index() as ViewResult;            
            Assert.IsNotNull(result);
        }

        [Test]
        public void MailTest()
        {
            MailerDbEntities _db = new MailerDbEntities();
            _db.Contacts.RemoveRange(_db.Contacts.Where(x => x.Email != null).AsEnumerable());
            var email1 = "aa@gmail.com";
            var email2 = "bb@gmail.com";
            var emailList = new List<string>
            {
                email1, email2
            };
            _db.Contacts.Add(new Contact()
            {
                Email = email1
            });
            _db.Contacts.Add(new Contact()
            {
                Email = email2
            });
            _db.SaveChanges();

            var homeController = new HomeController();
            homeController.SendAllEmail();
            Assert.AreEqual(2, _simpleSmtpServer.ReceivedEmailCount);
            Assert.IsTrue(emailList.Contains(_simpleSmtpServer.ReceivedEmail[0].ToAddresses[0].ToString()));
            Assert.IsTrue(emailList.Contains(_simpleSmtpServer.ReceivedEmail[1].ToAddresses[0].ToString()));

        }

    }
}
