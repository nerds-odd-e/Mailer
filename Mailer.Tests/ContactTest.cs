using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mailer.Models;
using NUnit.Framework;

namespace Mailer.Tests
{
    [TestFixture]
    public class ContactTest
    {
        [Test]
        public void ContactDelete()
        {
            List<Contact> result;
            using (var db = new MailerDbEntities())
            {
                var ctObj = new Contact()
                {
                    ID = 1,
                    Email = "Test2@321.com"
                };

                db.Entry(ctObj).State = EntityState.Added;
                db.SaveChanges();
                db.Entry(ctObj).State = EntityState.Deleted;
                db.SaveChanges();
                result = db.Contacts.Where(x => x.Email == "Test2@321.com").ToList();
                
            }
            Assert.IsTrue(result.Count == 0);
        }
    }
}
