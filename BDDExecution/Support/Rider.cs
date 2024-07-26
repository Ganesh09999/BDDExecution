using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace BDDExecution.Support
{
    public static class Rider
    {
        public static WebDriverWait GetWait(IWebDriver driver, int timeoutInSeconds = 20)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static IWebElement GetElement(this IWebDriver driver, By by) =>
            IsElementVisible(driver, by) ? driver.FindElement(by) : throw new Exception($"No element found! Better luck next time.");

        public static void ScrollToElement(this IWebDriver driver, By by)
        {
            try
            {
                var actions = new Actions(driver);
                actions.MoveToElement(driver.FindElement(by));
                actions.Perform();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsElementVisible(this IWebDriver driver, By by)
        {
            var wait = GetWait(driver);
            return wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(by);
                    return element.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    throw new Exception($"Element found staled/inactive by {by}!");
                }
                catch (NoSuchElementException)
                {
                    throw new Exception($"No element found by {by}!");
                }
            });
        }
    }
}
