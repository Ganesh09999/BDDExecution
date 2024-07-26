using BDDExecution.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace BDDExecution.Pages
{
    internal class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        By homePage_KnowMore_Button = By.XPath("//*[contains(text(),'Know')]");
        By homePage_Scroller_Icon = By.XPath("//*[@class=\"scroller\"]");

        public bool IsScrollerIconVisible => Rider.IsElementVisible(driver, homePage_Scroller_Icon); 

        public bool IsClickKnowButtonVisible()
        {
            Rider.ScrollToElement(driver, homePage_KnowMore_Button);
            return Rider.IsElementVisible(driver, homePage_KnowMore_Button);
        }

        public AboutUsPage ClickKnowMore()
        {
            Rider.GetElement(driver, homePage_KnowMore_Button).Click();
            return new AboutUsPage(driver);
        }
    }
}