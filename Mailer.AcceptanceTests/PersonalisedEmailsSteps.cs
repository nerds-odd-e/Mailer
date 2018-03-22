using System;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class PersonalisedEmailsSteps
    {
        [Given(@"(.*) (.*)'s email address (.*) is registered in system")]
        public void GivenXmanLogoSEmailAddressLxGmail_ComIsRegisteredInSystem(string firstName, string lastName, string email)
        {

            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Recepient's email starts with (.*)")]
        public void ThenRecepientSEmailStartsWith(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
