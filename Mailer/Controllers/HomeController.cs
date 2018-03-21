using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;
using Mailer.Services;

namespace Mailer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendAllMail()
        {
            try
            {
                var contacts = GetAllContact();
                SendEmail(contacts, new SmtpClientWrapper());
                return View(true);
            }
            catch
            {
                return View(false);
            }

        }

        private List<string> GetAllContact()
        {
            return new List<string>();
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