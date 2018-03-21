using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mailer.Controllers;
using Mailer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Mailer.Tests.Controllers
{
    [TestFixture()]
    public class ContactsControllerTests
    {
        private ContactsController controller;
        private MailerDbEntities _db ;
        [OneTimeSetUp]
        public void Init()
        {
            controller = new ContactsController();
            _db = new MailerDbEntities();
        }
        [Test()]
        public void IndexTest()
        {
            // Arrange
            

            // Act
            ViewResult result = controller.Index() as ViewResult;


            Assert.IsNotNull(result);
        }

        [Test()]
        public void DetailsTest()
        {
            // Arrange
            var result = controller.Details(GetContact().ID) as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test()]
        public void CreateTest()
        {
            // Arrange            
            // Act
            ViewResult result = controller.Create( new Contact()
            {
                Email = "testabc@gmail.com"
            }) as ViewResult;
            var contacts = _db.Contacts.First(x => x.Email == "testabc@gmail.com");           

            Assert.IsNotNull(contacts);
            _db.Contacts.Remove(contacts);
        }

        [Test()]
        public void EditTest_idIsNULL()
        {
            // Arrange
            
            int? id = null;
            HttpStatusCodeResult result = controller.Edit(id) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test()]
        public void EditTest_ContactIsNULL()
        {
            // Arrange            
            int? id = 0;
            HttpNotFoundResult result = controller.Edit(id) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test()]
        public void EditTest()
        {                           
            var result = controller.Edit(GetContact().ID) as ViewResult;
            Assert.IsNotNull(result);
        }

        private Contact GetContact()
        {

            if (!_db.Contacts.Any())
            {
                controller.Create(new Contact()
                {
                    Email = "UnitTest@Test.com"
                });
            }

            return _db.Contacts.First();
        }
        [Test()]
        public void DeleteTest_idIsNULL()
        {
            // Arrange

            int? id = null;
            HttpStatusCodeResult result = controller.Delete(id) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test()]
        public void DeleteTest_ContactIsNULL()
        {
            // Arrange            
            int? id = 0;
            HttpNotFoundResult result = controller.Delete(id) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }
        [Test()]
        public void DeleteTest()
        {
            var result = controller.Delete(GetContact().ID) as ViewResult;
            var resultContact = result.Model as Contact;
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultContact);

        }

        [Test()]
        public void DeleteConfirmedTest()
        {
            //Assert.Fail();
        }
    }
}