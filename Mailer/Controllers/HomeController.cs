using System;
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
                SendEmail(contacts);
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

        private ISmtpClient _client;

        public ISmtpClient Client
        {
            get { return _client; }
            set
            {
                var host = Environment.GetEnvironmentVariable("smtpServer");
                var port = int.Parse(Environment.GetEnvironmentVariable("smtpPort") ?? "25");
                var email = Environment.GetEnvironmentVariable("senderEmail");
                var password = Environment.GetEnvironmentVariable("senderPassword");
                value.EnableSsl();
                value.Initialize(host, port, email, password);
                _client = value;
            }
        }

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
}