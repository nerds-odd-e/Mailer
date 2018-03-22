using System;
using System.Collections.Generic;
using System.Net.Mail;
using Mailer.Controllers;
using Mailer.Models;
using Mailer.Services;
using NSubstitute;
using NUnit.Framework;

namespace Mailer.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest : WithDBAttribute
    {
        private ISmtpClient _fakeClient;
        private HomeController _homeController;

        [SetUp]
        public void Init()
        {
            _fakeClient = Substitute.For<ISmtpClient>();

            _homeController = new HomeController
            {
                Client = _fakeClient
            };
        }

        [Test]
        [Ignore("")]
        public void NoCourses_DoNotSendMail()
        {
            //            using (var db = new MailerDbEntities())
            //            {
            //                var courses = new List<Course>();
            //                for (int i = 0; i < number; i++)
            //                {
            //                    courses.Add(new Course
            //                    {
            //                        CourseName = i.ToString(),
            //                        StartDate = DateTime.Now,
            //                        EndDate = DateTime.Now
            //                    });
            //                }
            //                db.Courses.AddRange(courses);
            //                db.SaveChanges();
            //            }
            var contacts = new List<Contact>
            {
                new Contact {Email = "test@test.com"},
                new Contact {Email = "test2@test.com"}
            };
            db.Contacts.AddRange(contacts);
            db.SaveChanges();
            _homeController.SendAllMail();
            _fakeClient.Received(0).Send(Arg.Any<MailMessage>());
        }

        [Test]
        public void SendEmail()
        {
            var contacts = new List<Contact>
            {
                new Contact {Email = "test@test.com"},
                new Contact {Email = "test2@test.com"}
            };
            db.Contacts.AddRange(contacts);
            db.SaveChanges();
            _homeController.SendAllMail();
            _fakeClient.Received(2).Send(Arg.Any<MailMessage>());
        }
    }
}
