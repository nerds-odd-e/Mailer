using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Mailer.Models;
using Mailer.Services;

namespace Mailer.Controllers
{
    public class HomeController : Controller
    {
        private ISmtpClient _smtpClient;

        public ISmtpClient SmtpClient
        {
            get { return _smtpClient; }
            set
            {
                _smtpClient = new SmtpClientWrapper();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendAllMail()
        {
            try
            {
                var contacts = GetAllContact();
                SendEmail(contacts, _smtpClient);
                return View(true);
            }
            catch
            {
                return View(false);
            }

        }

        private List<string> GetAllContact()
        {
            var db = new MailerDbEntities();
            return db.Contacts.Select(x=> x.Email).ToList() ;
        }

        public void SendEmail(List<string> recipientList, ISmtpClient clientWrapper)
        {
            clientWrapper.EnableSsl();
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