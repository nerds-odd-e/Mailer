using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using Mailer.Models;
using netDumbster.smtp;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class SendMailToAllSteps
    {
        private readonly BrowserSession _browser;
        private readonly SimpleSmtpServer _smtpServer;
        private readonly MailerDbEntities _db;

        public SendMailToAllSteps(BrowserSession browser, SimpleSmtpServer smtpServer, MailerDbEntities db)
        {
            _browser = browser;
            _smtpServer = smtpServer;
            _db = db;
        }

        [Given(@"Upcoming course number is (.*)")]
        public void GivenUpcomingCourseNumberIs(int number)
        {
            List<Course> courses = new List<Course>();
            for (int i = 0; i < number; i++)
            {
                courses.Add(new Course
                {
                    CourseName = i.ToString(),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                });
            }
            _db.Courses.AddRange(courses);
            _db.SaveChanges();

        }

        [Given(@"I register a contact with email (.*)")]
        public void GivenIRegisterAContactWithEmail(string emailAddress)
        {
            _browser.Visit("http://localhost");
            _browser.ClickLink("Register Contact");
            _browser.ClickLink("Create New");

            var addresses = emailAddress.Split(';').ToList();
            addresses.ForEach(x =>
            {
                _browser.FillIn("Email").With(x.Replace("\"", ""));
                Assert.AreEqual(x.Replace("\"", ""), _browser.FindField("Email").Value);
                _browser.ClickButton("Create");
            });
        }

        [When(@"I press send email")]
        public void WhenIPressSendEmail()
        {
            _browser.Visit("http://localhost/Home");
            _browser.ClickButton("Send Email");
        }

        [Then(@"Email sent number should be (.*)")]
        public void ThenEmailSentNumberShouldBe(int emailCount)
        {
            Assert.AreEqual(emailCount,_smtpServer.ReceivedEmailCount);
        }
    }
}