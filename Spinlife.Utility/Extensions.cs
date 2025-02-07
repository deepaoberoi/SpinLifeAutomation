using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Spinlife.Utility
{
    public static class Extensions
    {
        public static IWebElement WaitForEnabled(this IWebElement element, int timeSpan = 10000)
        {
           Stopwatch watch = new Stopwatch();
           
           watch.Start();
           while (watch.Elapsed.Milliseconds < timeSpan)
           {
               if (element.Enabled)
                   return element;
           }

           throw new ElementNotInteractableException();
        }
        
        public static IWebElement WaitForVisible(this IWebElement element, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();
           
            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Displayed)
                    return element;
            }

            throw new ElementNotVisibleException();
        }
        
        public static IWebElement WaitForText(this IWebElement element, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();
           
            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Text.Length > 0)
                    return element;
            }

            throw new ElementNotVisibleException();
        }
        
        public static IWebElement WaitForText(this IWebElement element, string text, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();
           
            watch.Start();
            while (watch.Elapsed.Seconds < timeSpan)
            {
                if (element.Text == text)
                    return element;
            }

            throw new NoSuchElementException();
        }

        public static void Wait(IWebDriver driver, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            wait.Until(driver => element.Displayed);
        }

        public static IWebElement FindElementFluentWait(IWebDriver driver, string locator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);

            fluentWait.Timeout = TimeSpan.FromSeconds(20);

            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);

            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            fluentWait.Message = "Element "+ locator + "to be searched not found";

            IWebElement searchResult = fluentWait.Until(x => x.FindElement(By.XPath(locator)));

            return searchResult;
        }

    }
}