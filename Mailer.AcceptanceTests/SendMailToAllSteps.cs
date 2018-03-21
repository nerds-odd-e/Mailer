using Coypu;
using Mailer.Controllers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class SendMailToAllSteps
    {
        private readonly BrowserSession _browser;
        private int _course;
        private string _contactEmail;

        public SendMailToAllSteps(BrowserSession browser)
        {
            _browser = browser;
        }

        [Given(@"Upcoming course number is (.*)")]
        public void GivenUpcomingCourseNumberIs(int number)
        {
            _course = number;
        }

        [Given(@"I register a contact with email (.*)")]
        public void GivenIRegisterAContactWithEmail(string emailAddress)
        {
            _browser.Visit("http://localhost/Home");
            _browser.ClickLink("Contact");
            _browser.FillIn("email").With(emailAddress);
            Assert.AreEqual(emailAddress, _browser.FindField("email").Value);
            _contactEmail = emailAddress;
        }

        [When(@"I press send email")]
        public void WhenIPressSendEmail()
        {
            
            HomeController ctrl = new HomeController();
           
            ctrl.SendAllMail();
            

        }

        [Then(@"Email sent number should be (.*)")]
        public void ThenEmailSentNumberShouldBe(int emailCount)
        {
            
        }
    }
}