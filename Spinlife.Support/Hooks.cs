using System;
using BoDi;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.Extensions;
using AlerStallings.Factories;

namespace AlerStallings.Support
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
			//_driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
			_driver.Manage().Window.Maximize();
			_objectContainer.RegisterInstanceAs(_driver);
		}

		[AfterScenario]
		public void AfterScenario(ScenarioContext scenarioContext)
		{
			if (scenarioContext.TestError != null)
			{
				_driver.TakeScreenshot().SaveAsFile(Path.Combine("..", "..", "TestResults", $"{scenarioContext.ScenarioInfo.Title}.png"), ScreenshotImageFormat.Png);
			}
            _driver?.Dispose();
		}
	}
}
