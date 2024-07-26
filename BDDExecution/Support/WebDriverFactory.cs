using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace BDDExecution.Support
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }
    public class WebDriverFactory
    {
        public IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    //chromeOptions.AddArguments("--headless=new");
                    return GetChromeDriver(chromeOptions);
                case BrowserType.Firefox:
                    return GetFirefoxDriver();
                case BrowserType.Edge:
                    return GetEdgeDriver();
                default:
                    throw new ArgumentOutOfRangeException("Browser type does not exist...");
            }
        }

        private IWebDriver GetEdgeDriver()
        {
            return new EdgeDriver();
        }

        private IWebDriver GetFirefoxDriver()
        {
            return new FirefoxDriver();
        }

        private IWebDriver GetChromeDriver(ChromeOptions chromeOptions)
        {
            return new ChromeDriver(chromeOptions);
        }
    }
}
