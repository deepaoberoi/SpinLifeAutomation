using System;
using AlerStellings.Utility;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace AlerStallings.PageObjects
{
    public class SinglePageCheckout : PageObject
    {
        private readonly IWebDriver _driver;
        public SinglePageCheckout(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }
        public Boolean IsHomePageDisplayed()
        {
            return _driver.Title.Contains("SpinLife");
        }
        public void SearchForProduct(string product)
        {
            textboxSearch.SendKeys(product);
            textboxSearch.SendKeys(Keys.Enter);
        }
        public void ClickOnViewProduct()
        {
            buttonViewProduct.Click();
        }
        public Boolean ViewProductDetails()
        {
            return buttonAddToCart.Displayed;
        }
        public void ClickOnAddToCart()
        {
            buttonAddToCart.Click();
        }
        public Boolean IsMandatoryValidationDisplayed()
        {
            return labelUpholsteryMandatoryValidation.Displayed;
        }
        public void SelectUpholstery()
        {
            Extensions.WaitForVisible(dropDownUpholstery, 5000);
            dropDownUpholstery.Click();
            Extensions.WaitForVisible(dropDownUpholsteryValue, 5000);
            dropDownUpholsteryValue.Click();
        }

        public bool IsProductAddedToCart()
        {
            buttonNoThanks.Click();
            Extensions.WaitForVisible(buttonViewCart, 5000);
            return buttonViewCart.Displayed;
        }

        public void ClickOnViewCart()
        {
            Thread.Sleep(5000);
            Extensions.WaitForVisible(buttonViewCart, 5000);
            buttonViewCart.Click();
        }

        public void ClickOnProceedToCheckout()
        {
            Extensions.WaitForVisible(buttonProceedToCheckout, 5000);
            buttonProceedToCheckout.Click();
        }

        public void FillShippingDetails()
        {
            textboxFirstName.SendKeys(Faker.Name.First());
            textboxLastName.SendKeys(Faker.Name.Last());
            textboxAddress.SendKeys(Faker.Address.StreetAddress());
            textboxCity.SendKeys(Faker.Address.City());
            dropDownState.SendKeys(Faker.Address.UsState());
            textboxZip.SendKeys(Faker.Address.ZipCode());
            textBoxPhone.SendKeys(Faker.Phone.Number());
            chkBoxSameAsShipping.Click();
        }

        public void FillCreditCardDetails()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(rdButtonCC).Perform();
            rdButtonCC.Click();

            _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-number")));
            textBoxCreditCardNo.SendKeys("4111111111111111");
            _driver.SwitchTo().DefaultContent();

             _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-expirationMonth")));
            dropDownExpiryMonth.SendKeys("12");
            _driver.SwitchTo().DefaultContent();

             _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-expirationYear")));
            dropDownExpiryYear.SendKeys("2028");
            _driver.SwitchTo().DefaultContent();

             _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-cvv")));
            textBoxCVV.SendKeys("123");
            _driver.SwitchTo().DefaultContent();

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", chkBoxReturnPolicy);
            chkBoxReturnPolicy.Click();
        }

        public void SelectMailaCheckOrMoneyOrder()
        {
            rdButtonChequeOrMoneyOrder.Click();
        }
        public void ClickOnPlaceOrder()
        {
            btnPlaceOrder.Click();
            Thread.Sleep(5000);
        }

        public bool IsOrderConfirmationDisplayed()
        {
            Extensions.WaitForVisible(labelOrderConfirmation, 10000);
            Thread.Sleep(5000);
            return labelOrderConfirmation.Displayed;
        }

        public void BetteLogin()
        {
            textboxBetteEmail.SendKeys("deepa.oberoi@numotion.com");
            textboxBettePassword.SendKeys("1990@Diya");
            btnBetteLogin.Click();
        }

        private IWebElement textboxSearch => _driver.FindElement(By.XPath("//*[@id='search']"));
        private IWebElement buttonViewProduct => _driver.FindElement(By.XPath("//a[contains(text(),'View Product')]"));
        private IWebElement buttonAddToCart => _driver.FindElement(By.XPath("//a[contains(text(),'Add to cart')]"));
        private IWebElement labelUpholsteryMandatoryValidation => _driver.FindElement(By.XPath("//li[contains(text(),'Upholstery is a required field.')]"));
        private IWebElement dropDownUpholstery => _driver.FindElement(By.XPath("//*[@id='s_X845']"));
        private IWebElement dropDownUpholsteryValue => _driver.FindElement(By.XPath("//div[@class='select-custom-options']/div[2]"));
        private IWebElement buttonNoThanks => _driver.FindElement(By.XPath("//*[@id='noThanks']"));
        private IWebElement buttonViewCart => _driver.FindElement(By.XPath("//a[contains(text(),'View Cart')]"));
        private IWebElement buttonProceedToCheckout => _driver.FindElement(By.XPath("//a[contains(text(),'Proceed to Checkout')]"));
        private IWebElement textboxFirstName => _driver.FindElement(By.XPath("//*[@id='ship_first_name']"));
        private IWebElement textboxLastName => _driver.FindElement(By.XPath("//*[@id='ship_last_name']"));
        private IWebElement textboxAddress => _driver.FindElement(By.XPath("//*[@id='ship_street1']"));
        private IWebElement textboxCity => _driver.FindElement(By.XPath("//*[@id='ship_city']"));
        private IWebElement dropDownState => _driver.FindElement(By.XPath("//*[@id='ship_state']"));
        private IWebElement textboxZip => _driver.FindElement(By.XPath("//*[@id='ship_postal']"));
        private IWebElement textBoxPhone => _driver.FindElement(By.XPath("//*[@id='ship_phone']"));
        private IWebElement chkBoxSameAsShipping => _driver.FindElement(By.XPath("//*[@id='ship_bill']"));
        private IWebElement rdButtonCC => _driver.FindElement(By.CssSelector("label[for='payment_type1']"));
        private IWebElement rdButtonChequeOrMoneyOrder => _driver.FindElement(By.CssSelector("label[for='payment_type6']"));
        private IWebElement textBoxCreditCardNo => _driver.FindElement(By.XPath("//*[@id='credit-card-number']"));

        private IWebElement dropDownExpiryYear => _driver.FindElement(By.XPath("//*[@id='expiration-year']"));
        private IWebElement dropDownExpiryMonth => _driver.FindElement(By.XPath("//*[@id='expiration-month']"));
        private IWebElement textBoxCardFirstName => _driver.FindElement(By.XPath("//*[@id='card_first_name']"));
        private IWebElement textBoxCardLastName => _driver.FindElement(By.XPath("//*[@id='card_last_name']"));
        private IWebElement textBoxCVV => _driver.FindElement(By.XPath("//*[@id='cvv']"));
        private IWebElement chkBoxReturnPolicy => _driver.FindElement(By.Id("feReturnPolicy"));
        private IWebElement btnPlaceOrder => _driver.FindElement(By.XPath("//*[@id='submit-button']"));

        private IWebElement labelOrderConfirmation => _driver.FindElement(By.XPath("(//h1[contains(text(), 'Thank you')])"));
        private IWebElement textboxBetteEmail => _driver.FindElement(By.XPath("//*[@id='Username']"));
        private IWebElement textboxBettePassword => _driver.FindElement(By.XPath("//*[@id='Password']"));
        private IWebElement btnBetteLogin => _driver.FindElement(By.XPath("//*[@id='loginButton']"));



    }
}