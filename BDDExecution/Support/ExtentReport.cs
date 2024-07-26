using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;
using Serilog;
using Log = Serilog.Log;

namespace BDDExecution.Support
{
    public class ExtentReport
    {
        public static ExtentReports extentReport;
        public static ExtentTest feature;
        public static ExtentTest scenario;

        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        public static string testResultPath = Path.Combine(dir, $"Reports_{DateTime.Now.ToString("MMdd_HHmm")}");

        public static void StartReporter()
        {
            SetupSerilog();
            var htmlReporter = new ExtentSparkReporter(Path.Combine(testResultPath, "ExtentReport.html"));
            htmlReporter.Config.ReportName = "Automation Report";
            htmlReporter.Config.DocumentTitle = "Automation Status Report";
            htmlReporter.Config.Theme = Theme.Dark;

            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
            extentReport.AddSystemInfo("Application", "TestApp");
            extentReport.AddSystemInfo("Browser", "Chrome");
            extentReport.AddSystemInfo("OS", "Windows");
        }

        public static void ReportTestOutcome()
        {
            extentReport.Flush();
        }

        public string addScreenShot(IWebDriver driver, string fileName)
        {
            var getScreenShot = (ITakesScreenshot)driver;
            var screenShot = getScreenShot.GetScreenshot();
            var screenShotLocation = Path.Combine(testResultPath, fileName + ".png");
            screenShot.SaveAsFile(screenShotLocation);
            return screenShotLocation;
        }

        private static void SetupSerilog()
        {
            string LogReportFullPath = Path.Combine(testResultPath, "SeriLogs.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(LogReportFullPath,
                rollOnFileSizeLimit: true)
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}
