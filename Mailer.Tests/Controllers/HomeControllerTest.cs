using System;
using System.Collections.Generic;
using System.Linq;
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
        List<Contact> contacts = new List<Contact>
        {
            new Contact {Email = "test@test.com"},
            new Contact {Email = "test2@test.com"}
        };

        List<Course> courses = new List<Course>()
        {
            new Course
            {
                CourseName = "A_Course",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            },
            new Course
            {
                CourseName = "B_Course",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            }
        };

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
        public void NoCourses_DoNotSendMail()
        {
            db.Contacts.AddRange(contacts);
            db.SaveChanges();

            _homeController.SendAllMail();
            _fakeClient.Received(0).Send(Arg.Any<MailMessage>());
        }

        [Test]
        public void HaveCourses_SendMail()
        {
            db.Courses.AddRange(courses);
            db.Contacts.AddRange(contacts);
            db.SaveChanges();

            _homeController.SendAllMail();
            _fakeClient.Received(contacts.Count).Send(Arg.Any<MailMessage>());
        }

        [Test]
        public void NoContactsHasCourses_DoNotSendMail()
        {
            db.Courses.AddRange(courses);
            db.SaveChanges();

            _homeController.SendAllMail();
            _fakeClient.Received(0).Send(Arg.Any<MailMessage>());
        }
    }
}
