﻿using OpenQA.Selenium;

namespace Spinlife.Support
{
    class JsHelper
    {
        public static void ClickElementByJs(IWebDriver _driver, IWebElement webElement)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", webElement);
        }
    }
}
