using Coypu;
using Mailer.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class SendMailToAllSteps
    {
        private readonly BrowserSession _browser;

        public SendMailToAllSteps(BrowserSession browser)
        {
            _browser = browser;
        }

        [Given(@"Upcoming course number is (.*)")]
        public void GivenUpcomingCourseNumberIs(int p0)
        {
            ScenarioContext.Current.Add("CourseNumber", p0);
        }

        [Given(@"I register a contact with email ""(.*)""")]
        public void GivenIRegisterAContactWithEmail(string p0)
        {
            _browser.Visit("http://localhost/Home");
            _browser.ClickLink("Contact");
            _browser.FillIn("email").With(p0);
            Assert.AreEqual(p0, _browser.FindField("email").Value);
            ScenarioContext.Current.Add("ContactEmail", p0);
        }

        [When(@"I press send email")]
        public void WhenIPressSendEmail()
        {
            
            HomeController ctrl = new HomeController();
           
            ctrl.SendAllMail();
            
        }

        [Then(@"Email sent number should be (.*)")]
        public void ThenEmailSentNumberShouldBe(int p0)
        {
            
        }
    }
}