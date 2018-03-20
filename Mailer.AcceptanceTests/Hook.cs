using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using Coypu;
using Coypu.Drivers;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private BrowserSession _browser;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void Before()
        {
            _browser = new BrowserSession(new SessionConfiguration { AppHost = "localhost", Browser =Browser.Chrome });
            _objectContainer.RegisterInstanceAs(_browser);
        }


        [AfterScenario]
        public void AfterScenario()
        {
            _browser.Dispose();
        }
    }
}
