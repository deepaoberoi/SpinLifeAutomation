using System;
using BoDi;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.Extensions;
using Spinlife.Factories;

namespace Spinlife.Support
{
	[Binding]
	public class Hooks
	{
		private readonly IObjectContainer _objectContainer;
		private IWebDriver _driver;
		private static DriverFactory _driverFactory;

		public Hooks(IObjectContainer objectContainer)
		{
			_objectContainer = objectContainer;
		}

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			_driverFactory = new DriverFactory();
			Directory.CreateDirectory(Path.Combine("..", "..", "TestResults"));
		}

		[BeforeScenario(Order = 0)]
		public void BeforeScenario()
		{
			_driver = _driverFactory.CreateDriver();
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			_driver.Manage().Window.Maximize();
			_objectContainer.RegisterInstanceAs(_driver);
		}
		[AfterStep]
		public void AfterStep(ScenarioContext scenarioContext)
		{
			scenarioContext["LastStepText"] = scenarioContext.StepContext.StepInfo.Text;
		}

		[AfterScenario]
		public void AfterScenario(ScenarioContext scenarioContext)
		{
			// if (scenarioContext.TestError != null)
			// {
			// 	_driver.TakeScreenshot().SaveAsFile(Path.Combine("..", "..", "TestResults", $"{scenarioContext.ScenarioInfo.Title}.png"), ScreenshotImageFormat.Png);
			// }
			if (scenarioContext.TestError != null && _driver != null)
			{
				var stepText = scenarioContext.ContainsKey("LastStepText") ? scenarioContext["LastStepText"].ToString() : "UnknownStep";
				var safeStep = string.Concat(stepText.Split(Path.GetInvalidFileNameChars()));
				var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
				var screenshotsDir = Path.Combine("..", "..", "TestResults");
				Directory.CreateDirectory(screenshotsDir);
				var screenshotPath = Path.Combine(screenshotsDir, $"{safeStep}_{timestamp}.png");
				_driver.TakeScreenshot().SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
				
				// var screenshot = ((ITakesScreenshot)_driver).TakeScreenshot();
				// screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

				// For full page screenshot (Selenium 4+ only, Chrome/Firefox)
				// if (_driver is OpenQA.Selenium.Chrome.ChromeDriver chromeDriver)
				// {
				// 	var fullScreenshot = chromeDriver.GetFullPageScreenshot();
				// 	fullScreenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
				// }
				// else if (_driver is OpenQA.Selenium.Firefox.FirefoxDriver firefoxDriver)
				// {
				// 	var fullScreenshot = firefoxDriver.GetFullPageScreenshot();
				// 	fullScreenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
				// }
				// else
				// {
				// 	// fallback to normal screenshot
				// 	var screenshot = ((ITakesScreenshot)_driver).TakeScreenshot();
				// 	screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
				// }
				// Console.WriteLine($"Screenshot saved at: {screenshotPath}");
			}
            _driver?.Dispose();
		}
	}
}