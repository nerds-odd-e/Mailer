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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page testset.";

            return View();
        }

        private ISmtpClient _client;

        public ISmtpClient Client
        {
            get { return _client; }
            set
            {
                var password = "";
                value.Initialize("smtp.gmail.com", 587, "myodde@gmail.com", password);
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