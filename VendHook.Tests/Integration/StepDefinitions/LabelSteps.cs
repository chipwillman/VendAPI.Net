namespace VendHook.Tests.Integration.StepDefinitions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    using TechTalk.SpecFlow;

    using VendHook.Tests.Integration.Pages;

    [Binding]
    public class LabelSteps
    {
        private LabelPage labelPage;
        private IWebDriver driver;

        [BeforeScenario()]
        public void Setup()
        {
            driver = new FirefoxDriver();
        }

        [AfterScenario()]
        public void TearDown()
        {
            driver.Quit();
        }

        [Given(@"I am at the setup label page")]
        public void GivenIAmAtTheSetupLabelPage()
        {
            labelPage = LabelPage.NavigateTo(driver);
        }

        [When(@"I click on ""(.*)"" button")]
        public void WhenIClickOnAddButton(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the receipt template should contain ""(.*)""")]
        public void ThenTheReceiptTemplateShouldContain(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the ""(.*)"" button fields are below the ""(.*)"" fields")]
        public void ThenTheButtonFieldsAreBelowTheFields(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
