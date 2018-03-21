using Coypu;
using Mailer.Controllers;
using NUnit.Framework;
using netDumbster.smtp;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class SendMailToAllSteps
    {
        private readonly BrowserSession _browser;
        private int _course;
        private string _contactEmail;
        private SimpleSmtpServer _smtpServer;

        public SendMailToAllSteps(BrowserSession browser, SimpleSmtpServer smtpServer)
        {
            _browser = browser;
            _smtpServer = smtpServer;
        }

        [Given(@"Upcoming course number is (.*)")]
        public void GivenUpcomingCourseNumberIs(int number)
        {
            _course = number;
        }

        [Given(@"I register a contact with email (.*)")]
        public void GivenIRegisterAContactWithEmail(string emailAddress)
        {
            _browser.Visit("http://localhost");
            _browser.ClickLink("Register Contact");
            _browser.ClickLink("Create New");

            _browser.FillIn("Email").With(emailAddress.Replace("\"",""));
            Assert.AreEqual(emailAddress.Replace("\"", ""), _browser.FindField("Email").Value);
            _browser.ClickButton("Create");
        }

        [When(@"I press send email")]
        public void WhenIPressSendEmail()
        {
            _browser.Visit("http://localhost/Home");
            _browser.ClickLink("Send Email");
        }

        [Then(@"Email sent number should be (.*)")]
        public void ThenEmailSentNumberShouldBe(int emailCount)
        {
            Assert.AreEqual(emailCount, _smtpServer.ReceivedEmailCount);
        }
    }
}