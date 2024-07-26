using BDDExecution.Support;
using OpenQA.Selenium;

namespace BDDExecution.Pages
{
    internal class AboutUsPage
    {
        private IWebDriver driver;

        public AboutUsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        By heroImage = By.XPath("//*[contains(@class,'heroimage')]");

        public string GetDriverTitle => driver.Title;

        public bool IsHeroImageDisplayed()
        {
            return Rider.GetElement(driver, heroImage).Displayed;
        }
    }
}
