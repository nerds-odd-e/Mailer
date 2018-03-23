using System;
using Coypu;
using netDumbster.smtp;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class PersonalisedEmailsSteps
    {
        private readonly BrowserSession _browser;
        private readonly SimpleSmtpServer _smtpServer;

        public PersonalisedEmailsSteps(BrowserSession browser, SimpleSmtpServer smtpServer)
        {
            _browser = browser;
            _smtpServer = smtpServer;
        }

        [Given(@"(.*) (.*)'s email address (.*) is registered in system")]
        public void GivenXmanLogoSEmailAddressLxGmail_ComIsRegisteredInSystem(string firstName, string lastName, string email)
        {
            _browser.Visit("http://localhost");
            _browser.ClickLink("Register Contact");
            _browser.ClickLink("Create New");

            _browser.FillIn("FirstName").With(firstName.Replace("\"", ""));
            _browser.FillIn("LastName").With(lastName.Replace("\"", ""));
            _browser.FillIn("Email").With(email.Replace("\"", ""));
            _browser.ClickButton("Create");
        }
        
        [Then(@"Recepient's email starts with (.*)")]
        public void ThenRecepientSEmailStartsWith(string header)
        {
            Assert.IsTrue(_smtpServer.ReceivedEmail[0].MessageParts[0].BodyData.StartsWith(header));
        }
    }
}
