using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BDDExecution.Support;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using TechTalk.SpecFlow;

namespace BDDExecution.Hooks
{
    [Binding]
    public sealed class Hooks : ExtentReport
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private IWebDriver driver;
        private IObjectContainer container;
        private WebDriverWait wait;

        public Hooks(IObjectContainer container)
        {
            this.container = container;
            //wait = Rider.GetWait(driver);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Running BeforeTestRun...");
            StartReporter();
            Log.Information("Reports and Logs intialized...!!!");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Log.Information("**************************************************************************");
            Console.WriteLine("Running BeforeFeature...");
            feature = extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            Log.Information($"Feature {featureContext.FeatureInfo.Title} intialized...!!!");
        }

        [BeforeScenario("@demo ")]
        public static void BeforeScenarioWithTag(ScenarioContext scenarioContext)
        {
            Log.Information("#################################################");
            Console.WriteLine("Running BeforeScenarioWithTag...");
            Log.Information($"{scenarioContext.ScenarioInfo.Title} with tag {scenarioContext.ScenarioInfo.Tags.Select(c => "demo")} step is ready to execute...!!!");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            Log.Information("#################################################");
            Console.WriteLine("Running FirstBeforeScenario...");
            var factory = new WebDriverFactory();
            driver = factory.Create(BrowserType.Chrome);
            container.RegisterInstanceAs<IWebDriver>(driver);
            scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            Log.Information($"{scenarioContext.ScenarioInfo.Title} step ready to execute...!");
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running BeforeStep...");
            Log.Information($"{scenarioContext.StepContext.StepInfo.Text} step ready to execute...!");
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running AfterStep...");
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepName = scenarioContext.StepContext.StepInfo.Text;

            if (scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        scenario.CreateNode<Given>(stepName);
                        Log.Information($"Node created for {stepName}");
                        break;
                    case "When":
                        scenario.CreateNode<When>(stepName);
                        Log.Information($"Node created for {stepName}");
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(stepName);
                        Log.Information($"Node created for {stepName}");
                        break;
                    case "And":
                        scenario.CreateNode<And>(stepName);
                        Log.Information($"Node created for {stepName}");
                        break;
                    case "But":
                        scenario.CreateNode<But>(stepName);
                        Log.Information($"Node created for {stepName}");
                        break;
                    default:
                        Console.WriteLine("No Step Type was recognised in this scenario context");
                        Log.Information("No Step Type was recognised in this scenario context...");
                        break;
                }
            }

            if (scenarioContext.TestError != null)
            {
                switch (stepType)
                {
                    case "Given":
                        scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, stepName)).Build());
                        Log.Error($"{stepName} failed due to {scenarioContext.TestError.Message}.");
                        break;
                    case "When":
                        scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, stepName)).Build());
                        Log.Error($"{stepName} failed due to {scenarioContext.TestError.Message}.");
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, stepName)).Build());
                        Log.Error($"{stepName} failed due to {scenarioContext.TestError.Message}.");
                        break;
                    case "And":
                        scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, stepName)).Build());
                        Log.Error($"{stepName} failed due to {scenarioContext.TestError.Message}.");
                        break;
                    case "But":
                        scenario.CreateNode<But>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, stepName)).Build());
                        Log.Error($"{stepName} failed due to {scenarioContext.TestError.Message}.");
                        break;
                    default:
                        Console.WriteLine("No Step Type was recognised in this scenario context");
                        Log.Error("No Step Type was recognised in this scenario context...");
                        break;
                }
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running AfterScenario...");
            driver = container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }

            Log.Information($"{scenarioContext.ScenarioInfo.Title} got executed and driver got quit...!");
            Log.Information("#################################################");
        }
        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running AfterFeature...");
            Log.Information($"{featureContext.FeatureInfo.Title} completed...!");
            Log.Information("**************************************************************************");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running AfterTestRun...");
            ReportTestOutcome();
            Log.Information("Reports and Logs completed...!");
        }
    }
}