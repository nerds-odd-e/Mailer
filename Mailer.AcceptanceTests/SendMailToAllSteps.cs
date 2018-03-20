using System;
using Mailer.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class SendMailToAllSteps
    {
        [Given(@"Upcoming course number is (.*)")]
        public void GivenUpcomingCourseNumberIs(int p0)
        {
            ScenarioContext.Current.Add("CourseNumber",p0);
        }
        
        [Given(@"I register a contact with email ""(.*)""")]
        public void GivenIRegisterAContactWithEmail(string p0)
        {
            ScenarioContext.Current.Add("ContactEmail",p0);
        }
        
        [When(@"I press send email")]
        public void WhenIPressSendEmail()
        {
            //EmailService service = new EmailService();
            //service.ContactEmail = ScenarioContext.Current.Get<string>("ContactEmail");
            //Actual value
            int sentNumber= 1;

            ScenarioContext.Current.Add("SentNumber",sentNumber);
        }
        
        [Then(@"Email sent number should be (.*)")]
        public void ThenEmailSentNumberShouldBe(int p0)
        {
            //Actual
            var actual = ScenarioContext.Current.Get<int>("SentNumber");
            //Except value
            var expect= p0;
            Assert.AreEqual(expect,actual);
        }
    }
}
