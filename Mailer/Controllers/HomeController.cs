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
                var myoddeEmailPassword = Environment.GetEnvironmentVariable("myoddeEmailPassword");
                var gmailServer = Environment.GetEnvironmentVariable("gmailServer");
                var gmailServerPort = int.Parse(Environment.GetEnvironmentVariable("gmailServerPort"));
                var myoddeSenderEmail = Environment.GetEnvironmentVariable("myoddeSenderEmail");
                value.EnableSsl();
                value.Initialize(gmailServer, gmailServerPort, myoddeSenderEmail, myoddeEmailPassword);
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