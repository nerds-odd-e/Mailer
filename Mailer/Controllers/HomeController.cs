﻿using System.Collections.Generic;
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


        public ISmtpClient Client { get; set; }

        public void SendEmail(List<string> recipientList)
        {
            var password = "";
            Client.Initialize("smtp.gmail.com", 587, "myodde@gmail.com", password);
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