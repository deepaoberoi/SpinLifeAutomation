using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Spinlife.Factories
{
    public class DriverFactory
    {
        public IWebDriver CreateDriver()
        {
            string browser = Environment.GetEnvironmentVariable("BROWSER") ?? "CHROME";

            switch (browser.ToUpperInvariant())
            {
                case "CHROME":
                    var options = new ChromeOptions();
                    options.AddArguments("--incognito");
                    return new ChromeDriver(options);
                case "FIREFOX":
                    return new FirefoxDriver();
                default:
                    throw new ArgumentException($"Browser not yet implemented: {browser}");
            }
        }
    }
}