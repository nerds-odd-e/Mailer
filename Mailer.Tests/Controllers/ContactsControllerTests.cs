using System.Linq;
using System.Web.Mvc;
using Mailer.Controllers;
using Mailer.Models;
using NUnit.Framework;

namespace Mailer.Tests.Controllers
{
    [TestFixture]
    public class ContactsControllerTests
    {
        private ContactsController _controller;
        private MailerDbEntities _db ;
        [OneTimeSetUp]
        public void Init()
        {
            _controller = new ContactsController();
            _db = new MailerDbEntities();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public void IndexTest()
        {
            ViewResult result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void DetailsTest()
        {
            var result = _controller.Details(GetContact().ID) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateTest()
        {
            _controller.Create( new Contact
            {
                Email = "testabc@gmail.com"
            });
            var contacts = _db.Contacts.First(x => x.Email == "testabc@gmail.com");
            _db.Contacts.Remove(contacts);
            Assert.IsNotNull(contacts);
        }

        [Test]
        public void EditTest_idIsNULL()
        {
            var result = _controller.Edit((int?) null) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test]
        public void EditTest_ContactIsNULL()
        {
            int? id = 0;
            var result = _controller.Edit(id) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void EditTest()
        {          
            var result = _controller.Edit(GetContact().ID) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void EditTestContactIsInValid()
        {
            var contact = new Contact
            {
                Email = null
            };
            Assert.Throws<System.Data.Entity.Validation.DbEntityValidationException>(()=>_controller.Edit(contact));
        }
        [Test]
        public void EditTestContactIsValid()
        {
            var result = _controller.Edit(GetContact().ID) as ViewResult;
            Assert.AreEqual(true, _controller.ModelState.IsValid);
        }

        [Test]
        public void EditTestContact()
        {
            var contact = GetContact();
            string expected = "test1@gmail.com";
            contact.Email = expected;
            var result = _controller.Edit(GetContact()) as RedirectToRouteResult;
            var resContact = _db.Contacts.First(x => x.ID == contact.ID);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, resContact.Email);


        }
        private Contact GetContact()
        {
            if (!_db.Contacts.Any())
            {
                _controller.Create(new Contact
                {
                    Email = "UnitTest@Test.com"
                });
            }

            return _db.Contacts.First();
        }
        [Test]
        public void DeleteTest_idIsNULL()
        {
            int? id = null;
            HttpStatusCodeResult result = _controller.Delete(id) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test]
        public void DeleteTest_ContactIsNULL()
        {
            int? id = 0;
            HttpNotFoundResult result = _controller.Delete(id) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }
        [Test]
        public void DeleteTest()
        {
            var result = _controller.Delete(GetContact().ID) as ViewResult;
            var resultContact = result.Model as Contact;
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultContact);

        }

        [Test]
        public void DeleteConfirmedTest()
        {
            int id = GetContact().ID;
            _controller.DeleteConfirmed(id);

            var result = _db.Contacts.Count(x => x.ID == id);
            Assert.AreEqual(0,result);
        }
    }
}