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
            _db.Contacts.RemoveRange(_db.Contacts.Where(x => x.Email != null).AsEnumerable());
            _db.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public void CreateContact_ContactIsAdded()
        {
            _controller.Create( new Contact
            {
                Email = "testabc@gmail.com"
            });
            var contacts = _db.Contacts.First(x => x.Email == "testabc@gmail.com");
            Assert.IsNotNull(contacts);
        }

        [Test]
        public void EditContact_IdIsNULL()
        {
            var result = _controller.Edit((int?) null) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test]
        public void EditContact_ContactIsNULL()
        {
            var result = _controller.Edit(0) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void EditContact_ViewExist()
        {          
            var result = _controller.Edit(GetContact().ID) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void EditContact_NewContactIsInValid()
        {
            var contact = new Contact
            {
                Email = null
            };
            Assert.Throws<System.Data.Entity.Validation.DbEntityValidationException>(()=>_controller.Edit(contact));
        }

        [Test]
        public void EditContact_NewContactIsValid()
        {
            _controller.Edit(GetContact().ID);
            Assert.AreEqual(true, _controller.ModelState.IsValid);
        }

        [Test]
        public void EditContact_ContactEmailShouldUpdate()
        {
            var contact = GetContact();
            string newEmail = "test1@gmail.com";
            contact.Email = newEmail;
            var newContact = _controller.Edit(GetContact());
            var actualContact = _db.Contacts.First(x => x.ID == contact.ID);
            Assert.IsNotNull(newContact);
            Assert.AreEqual(newEmail, actualContact.Email);
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
        public void DeleteContact_IdIsNULL()
        {
            HttpStatusCodeResult result = _controller.Delete(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }
        [Test]
        public void DeleteContact_ContactIsNULL()
        {
            HttpNotFoundResult result = _controller.Delete(0) as HttpNotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }
        [Test]
        public void DeleteContact_ViewExist()
        {
            var result = _controller.Delete(GetContact().ID) as ViewResult;
            var resultContact = result.Model as Contact;
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultContact);
        }

        [Test]
        public void DeleteConfirmed()
        {
            var id = GetContact().ID;
            _controller.DeleteConfirmed(id);
            var result = _db.Contacts.Count(x => x.ID == id);
            Assert.AreEqual(0,result);
        }
    }
}