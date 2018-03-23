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
            var db = new MailerDbEntities();
            var contacts = db.Contacts.ToList();
            var courses = db.Courses.ToList();

            if (!courses.IsNullOrEmpty())
            {
                var emailer = new Emailer(Client);
                emailer.SendEmail(contacts);
            }
            return View(true);

        }
    }
}