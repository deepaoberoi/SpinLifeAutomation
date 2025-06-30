using System;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Spinlife.Config;
using Spinlife.Utility;

namespace Spinlife.PageObjects
{
    public class CartPageFunction : PageObject
    {
        private readonly IWebDriver _driver;
        public CartPageFunction(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }
        public void verifyProductPriceCorrect(string expectedPrice, string quantity)
        {
            string actualPrice = orderSummaryTotal.Text.Trim();
            actualPrice = Regex.Replace(actualPrice, @"\s+", " "); // Normalize whitespace
            actualPrice = actualPrice.Replace("$", ""); // Remove dollar sign for comparison
            actualPrice = actualPrice.Replace(",", ""); // Remove commas for comparison
            decimal actualPriceDecimal = decimal.Parse(actualPrice);
            decimal quantityValue = decimal.Parse(quantity);
            Console.WriteLine($"Actual Price: {actualPriceDecimal * quantityValue}");
            expectedPrice = expectedPrice.Replace("$", ""); // Remove dollar sign from expected price
            expectedPrice = expectedPrice.Replace(",", ""); // Remove commas for comparison
            decimal expectedPriceDecimal = decimal.Parse(expectedPrice);
            Console.WriteLine($"Expected Price: {expectedPriceDecimal}");
            Assert.AreEqual(expectedPriceDecimal, actualPriceDecimal * quantityValue, "The product price does not match the expected price.");
        }
        public void verifyCouponCode(string couponCode)
        {
            IWebElement couponCodeElement = _driver.FindElement(By.XPath("//input[@id='promo-input']"));
            couponCodeElement.SendKeys(couponCode);
            Thread.Sleep(2000); // Wait for the input to be processed
            IWebElement applyButton = _driver.FindElement(By.XPath("//a[@id='promo-submit']"));
            applyButton.Click();
            Thread.Sleep(2000); // Wait for the coupon to be applied
        }
        public bool verifyDiscountAppliedToCartTotal(string quantity)
        {
            string actualPriceText = discountElement.Text.Trim();
            actualPriceText = Regex.Replace(actualPriceText, @"\s+", " ");
            actualPriceText = actualPriceText.Replace("$", "");
            actualPriceText = actualPriceText.Replace(",", "");
            decimal actualPrice = decimal.Parse(actualPriceText);
            Console.WriteLine($"Actual Price: {actualPrice}");
            // Calculate 10% discount
            decimal discountValue = actualPrice * 0.10m;
            Console.WriteLine($"Discount Value: {discountValue}");
            // Get the total price after discount
            string totalText = orderSummaryTotal.Text.Trim();
            totalText = Regex.Replace(totalText, @"\s+", " ");
            totalText = totalText.Replace("$", "");
            totalText = totalText.Replace(",", "");
            decimal total = decimal.Parse(totalText);
            Console.WriteLine($"Total Price: {total}");
            // Calculate expected total
            decimal quantityValue = decimal.Parse(quantity);
            decimal expectedTotal = (actualPrice * quantityValue) - (discountValue * quantityValue);
            Console.WriteLine($"Expected Total after discount: {expectedTotal}");
            // Log the values for debugging
            // Compare the expected total with the actual total
            bool isDiscountApplied = Math.Abs(expectedTotal - total) < 0.01m;
            return isDiscountApplied;
        }

        public bool removeProductFromCart()  
        {
            IWebElement removeButton = _driver.FindElement(By.XPath("//a[@class='remove-link']"));
            removeButton.Click();
            Thread.Sleep(2000); // Wait for the product to be removed
            IAlert alert = _driver.SwitchTo().Alert();
            alert.Accept(); // Clicks "OK" on the popup
            Thread.Sleep(2000);
            bool isDisplayed = cartEmptyMessage.Displayed;
            return isDisplayed;
        }

        public void verifyCartIsEmpty()
        {   
            _driver.Navigate().Refresh();
            string expectedMessage = "You have 0 items in your shopping cart.";
            string actualMessage = cartEmptyMessage.Text.Trim();
            actualMessage = Regex.Replace(actualMessage, @"\s+", " "); // Normalize whitespace
            Assert.AreEqual(expectedMessage, actualMessage, "The cart is not empty.");
        }

        private IWebElement orderSummaryTotal => _driver.FindElement(By.XPath("//div[@id='total-price']"));
        private IWebElement discountElement => _driver.FindElement(By.XPath("//div[@class='sub-price-col']"));
        private IWebElement cartEmptyMessage => _driver.FindElement(By.XPath("//h2[@id='num-items-cart']"));


        // private IWebElement orderSummaryTotal => _driver.FindElement(By.XPath("//div[@id='total-price']"));

    }
}    
