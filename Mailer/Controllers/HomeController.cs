using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Mailer.Models;
using Mailer.Services;

namespace Mailer.Controllers
{
    public class Emailer
    {
        public Emailer(ISmtpClient client)
        {
            Client = client;
        }

        public ISmtpClient Client { get; set; }

        public void SendEmail(List<string> recipientList)
        {
            foreach (var recipient in recipientList)
            {
                var mail = new MailMessage("myodde@gmail.com", recipient)
                {
                    Subject = "this is a test email.",
                    Body = "this is my test email body"
                };
                Client.Send(mail);
            }
        }
    }

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
            var Emailer = new Emailer(Client);
            Emailer.SendEmail(contacts);
            return View();

        }

        private List<string> GetAllContact()
        {
            var db = new MailerDbEntities();
            return db.Contacts.Select(x=> x.Email).ToList() ;
        }

        public void SendEmail(List<string> recipientList, ISmtpClient clientWrapper)
        {
            foreach (var recipient in recipientList)
            {
                var mail = new MailMessage("myodde@gmail.com", recipient)
                {
                    Subject = "this is a test email.",
                    Body = "this is my test email body"
                };
                clientWrapper.Send(mail);
            }
        }
    }
}