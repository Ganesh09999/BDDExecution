using BDDExecution.Pages;
using BDDExecution.Support;
using FluentAssertions;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BDDExecution.StepDefinitions
{
    [Binding]
    public sealed class SearchStepDefinitions
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        HomePage homepage;

        public SearchStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            homepage = new(this.driver);
            wait = Rider.GetWait(driver);
        }

        [Given(@"Open the browser")]
        public void GivenOpenTheBrowser()
        {
            driver.Manage().Window.Maximize();
        }

        [Given(@"Navigate to URL")]
        [When(@"Navigate to URL")]
        public void WhenNavigateToURL()
        {
            driver.Url = "https://kaaratech.com/";
            wait.Until(c => c.Title.Contains("Kaara"));
        }

        [Then(@"Verify home page is displayed")]
        public void ThenVerifyHomePageIsDisplayed()
        {
            var element = driver.FindElement(By.ClassName("menu-bar"));
            //Thread.Sleep(5000);
            //ClassicAssert.IsTrue(element.Displayed);
            wait.Until(c => homepage.IsScrollerIconVisible);
            element.Displayed.Should().BeTrue();
        }

        [Then(@"Verify Contact details are displayed")]
        public void ThenVerifyContactDetailsAreDisplayed()
        {
            driver.FindElement(By.ClassName("menu-bar")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.LinkText("Contact")).Click();
            Thread.Sleep(5000);
            ClassicAssert.IsTrue(driver.FindElement(By.Id("btn_send")).Displayed, "Element is not displayed");
        }

        [Then(@"Verify About us are displayed")]
        public void ThenVerifyAboutUsAreDisplayed()
        {
            driver.FindElement(By.ClassName("menu-bar")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.LinkText("About Us")).Click();
            Thread.Sleep(5000);
            ClassicAssert.IsTrue(driver.FindElement(By.ClassName("material-symbols-outlined")).Displayed, "Element is not displayed");
        }

        [Then(@"Verify '([^']*)' is displayed")]
        public void ThenVerifyIsDisplayed(string page)
        {
            driver.FindElement(By.ClassName("menu-bar")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.LinkText(page)).Click();
            Thread.Sleep(5000);
            ClassicAssert.IsTrue(driver.Url.Contains(new string(page.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray())), "Page is not displayed");
        }

        [Then(@"Verify Pages are displayed")]
        public void ThenVerifyPagesAreDisplayed(Table table)
        {
            var pages = table.CreateSet<TestData>();

            foreach (var item in pages)
            {
                driver.FindElement(By.ClassName("menu-bar")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.LinkText(item.Page)).Click();
                Thread.Sleep(5000);
                ClassicAssert.IsTrue(driver.Url.Contains(new string(item.Page.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray())), $"{item.Page} is not displayed");
            }
        }
    }
}
