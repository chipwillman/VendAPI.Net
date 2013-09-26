namespace VendHook.Tests.Integration.Pages
{
    using OpenQA.Selenium;

    public class LabelPage
    {
        private static IWebDriver driver;

        public static LabelPage NavigateTo(IWebDriver webDriver)
        {
            driver = webDriver;
            driver.Navigate().GoToUrl("http://localhost/VendHook/Setup/Label");
            var labelPage = new LabelPage();
            labelPage.GetElements();
            return labelPage;
        }

        public void GetElements()
        {
            if (driver != null)
            {
                var freeTextValue = driver.FindElement(By.Id("free_text_value")).Text;
            }
        }
    }
}
