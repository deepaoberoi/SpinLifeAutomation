using FluentAssertions;
using OpenQA.Selenium;
using System;

namespace AlerStallings.PageObjects
{
    public abstract class PageObject
    {
        private readonly IWebDriver _driver;

        public PageObject(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(url);
          
        }

        public void AssertTitle(string title)
        {
            string pageTitle = _driver.Title;
            pageTitle.Should().Be(title);
        }

        public Boolean FindHeader(string header)
        {
            try
            {
                _driver.FindElement(By.XPath("//h2[normalize-space()='" + header + "']"));
                return true;
            }
            catch (Exception NoSuchElementException)
            {
                Console.WriteLine(NoSuchElementException.Message);
                return false;
            }
        }

        public Boolean FindDocumentHeader(string header)
        {
            try
            {
                _driver.FindElement(By.XPath("//h4[normalize-space()='" + header + "']"));
                return true;
            }
            catch (Exception NoSuchElementException)
            {
                Console.WriteLine(NoSuchElementException.Message);
                return false;
            }
        }

        public string GetLoggedInUser()
        {
            string userName = lUserName.Text.Trim();
            return userName;
        }

        private IWebElement lUserName => _driver.FindElement(By.XPath("//*[@id='navbarSupportedContent']/ul/li[5]/a"));

    }
}