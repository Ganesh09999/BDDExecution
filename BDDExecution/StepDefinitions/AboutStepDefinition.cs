using BDDExecution.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace BDDExecution.StepDefinitions
{
    [Binding]
    internal class AboutStepDefinition
    {
        private IWebDriver driver;
        HomePage homepage;
        AboutUsPage aboutPage;

        public AboutStepDefinition(IWebDriver driver)
        {
            this.driver = driver;
            homepage = new(this.driver);
            aboutPage = new(this.driver);
        }

        [When(@"Know more button is visible")]
        public void WhenKnowMoreButtonIsVisible()
        {
            Assert.That(homepage.IsClickKnowButtonVisible());
        }

        [When(@"Know more button is clicked")]
        public void WhenKnowMoreButtonIsClicked()
        {
            homepage.ClickKnowMore();
        }

        [Then(@"About us page is displayed")]
        public void ThenAboutUsPageIsDisplayed()
        {
            Assert.That(aboutPage.IsHeroImageDisplayed);
            //Assert.That(aboutPage.GetDriverTitle, Is.EqualTo("About Us | Kaara"));
            aboutPage.GetDriverTitle.Should().BeEquivalentTo("About Us | Kaara");
        }
    }
}
