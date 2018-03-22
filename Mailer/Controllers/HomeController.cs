using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using Mailer.Models;
using Mailer.Services;

namespace Mailer.Controllers
{
    public class HomeController : Controller
    {
        public ISmtpClient Client { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendAllMail()
        {
            var contacts = GetAllContact();
            List<Course> courses;
            using (var mailDb = new MailerDbEntities())
            {
                courses = mailDb.Courses.ToList();
            }

            if (!courses.IsNullOrEmpty())
            {
                var emailer = new Emailer(Client);
                emailer.SendEmail(contacts);
            }
            return View();

        }

        private List<string> GetAllContact()
        {
            var db = new MailerDbEntities();
            return db.Contacts.Select(x=> x.Email).ToList() ;
        }
    }
}