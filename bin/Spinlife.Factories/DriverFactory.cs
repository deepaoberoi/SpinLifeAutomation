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
                    options.AddArgument("--disable-popup-blocking");
                    options.AddArgument("--window-size=1920,1080");
                    return new ChromeDriver(options);
                case "FIREFOX":
                    return new FirefoxDriver();
                default:
                    throw new ArgumentException($"Browser not yet implemented: {browser}");
            }
        }
    }
}