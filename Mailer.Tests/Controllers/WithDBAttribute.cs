using Mailer.Models;
using NUnit.Framework;

namespace Mailer.Tests.Controllers
{
    public partial class WithDBAttribute
    {
        protected MailerDbEntities db;

        [SetUp]
        public void ConnectDB()
        {
            db = new MailerDbEntities();
        }

        [SetUp]
        public void CleanDb()
        {
            db.Database.ExecuteSqlCommand("delete from Contact");
            db.Database.ExecuteSqlCommand("delete from Course");
        }

        [TearDown]
        public void DisposeDB()
        {
            db.Dispose();
        }
    }
}