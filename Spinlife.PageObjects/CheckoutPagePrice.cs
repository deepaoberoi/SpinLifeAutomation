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
    public class CheckoutPagePrice : PageObject
    {
        private readonly IWebDriver _driver;
        public CheckoutPagePrice(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public void changeCookie()
        {
            _driver.Navigate().GoToUrl("https://staging.spinlife.biz/shopping/cartnew.cfm");
            Thread.Sleep(2000); // Allow some time for cookies to load
            var cookies = _driver.Manage().Cookies.AllCookies;
            foreach (var cookie in cookies)
            {
                Console.WriteLine($"{cookie.Name}: {cookie.Value}");
            }
            string cookieName = "_vis_opt_exp_14_combi";
            Cookie existingCookie = _driver.Manage().Cookies.GetCookieNamed(cookieName);
            if (existingCookie != null)
            {
                // Delete the existing cookie
                _driver.Manage().Cookies.DeleteCookie(existingCookie);

                // Create a new cookie with value "2"
                Cookie newCookie = new Cookie(cookieName, "2", ".spinlife.biz", "/", DateTime.Now.AddYears(1));

                // Add the modified cookie back to the browser
                _driver.Manage().Cookies.AddCookie(newCookie);

                //Console.WriteLine($"Updated Cookie: {cookieName} = 2");
                var updatedCookie = _driver.Manage().Cookies.GetCookieNamed(cookieName);
                Console.WriteLine($"Updated Cookie Value: {updatedCookie?.Value}");
                foreach (var cookie in cookies)
                {
                    Console.WriteLine($"{cookie.Name}: {cookie.Value}");
                }
            }
            else
            {
                Console.WriteLine("Cookie not found!");
            }

            // Refresh the page to apply the new cookie
            _driver.Navigate().Refresh();
        }
        public void SearchForProduct(string product)
        {
            textboxSearch.SendKeys(product);
            textboxSearch.SendKeys(Keys.Enter);
        }
        public void ClickOnViewProduct()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttonViewProduct);
            Extensions.WaitForVisible(buttonViewProduct, 5000);
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
            Thread.Sleep(2000);
            Extensions.WaitForVisible(dropDownUpholstery, 5000);
            dropDownUpholstery.Click();
            Extensions.WaitForVisible(dropDownUpholsteryValue, 5000);
            dropDownUpholsteryValue.Click();
        }
        public void SelectDropdownValues()
        {
            Thread.Sleep(2000);
            Extensions.WaitForVisible(dropDownFeatherChairColor, 5000);
            dropDownFeatherChairColor.Click();
            Extensions.WaitForVisible(dropDownFeatherChairColorValue, 5000);
            dropDownFeatherChairColorValue.Click();
            Thread.Sleep(2000);
            Extensions.WaitForVisible(dropDownFeatherChairLegrest, 5000);
            dropDownFeatherChairLegrest.Click();
            Extensions.WaitForVisible(dropDownFeatherChairLegrestValue, 5000);
            dropDownFeatherChairLegrestValue.Click();

        }

        public bool SelectFeatherScooter()
        {
            return true;
        }
        public void SelectViperScooter()
        {
            Thread.Sleep(2000);
            Extensions.WaitForVisible(dropdownViper, 5000);
            dropdownViper.Click();
            Extensions.WaitForVisible(dropdownViperValue, 5000);
            dropdownViperValue.Click();
            Thread.Sleep(2000);
            Extensions.WaitForVisible(dropdownViperLegrests, 5000);
            dropdownViperLegrests.Click();
            Extensions.WaitForVisible(dropdownViperLegrestsValue, 5000);
            dropdownViperLegrestsValue.Click();
        }

        public bool IsProductAddedToCart()
        {
            // Re-locate buttonNoThanks before using
            var buttonNoThanks = _driver.FindElement(By.XPath("//*[@id='noThanks']"));
            if (buttonNoThanks.Displayed)
            {
                Extensions.WaitForVisible(buttonNoThanks, 5000);
                buttonNoThanks.Click();
            }

            // Re-locate buttonViewCart before using
            var buttonViewCart = _driver.FindElement(By.XPath("//a[contains(text(),'View Cart')]"));
            Extensions.WaitForVisible(buttonViewCart, 5000);
            return buttonViewCart.Displayed;
        }

        public void ClickOnViewCart()
        {
            Thread.Sleep(2000);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttonViewCart);
            Extensions.WaitForVisible(buttonViewCart, 10000);
            buttonViewCart.Click();
        }

        public void ClickOnProceedToCheckout()
        {
            Extensions.WaitForVisible(buttonProceedToCheckout, 5000);
            buttonProceedToCheckout.Click();
            _driver.Navigate().GoToUrl("https://staging.spinlife.biz/shopping/checkout.cfm");
        }

        public void FillShippingDetails()
        {
            textboxFirstName.SendKeys(Faker.Name.First());
            textboxLastName.SendKeys(Faker.Name.Last());
            textboxAddress.SendKeys(Faker.Address.StreetAddress());
            textboxCity.SendKeys(Faker.Address.City());
            dropDownState.SendKeys(Faker.Address.UsState());

            string[] validUSZipCodes =
        {
            "10001", "30301", "60601", "75201", // Major city ZIPs
            "33101", "20001", "48201", "85001", // More city ZIPs
            "99501", "96801", "72201", "53701", "63101"  // Includes Alaska & Hawaii
        };

            Random random = new Random();
            string postalCode = validUSZipCodes[random.Next(validUSZipCodes.Length)];
            textboxZip.SendKeys(postalCode);
            textBoxPhone.SendKeys(Faker.Phone.Number());
            chkBoxSameAsShipping.Click();
        }
        public void VerifyCheckoutPage()
        {
            Extensions.WaitForVisible(emailTextbox, 5000);
            if (emailTextbox.GetAttribute("value").Length == 0)
            {
                emailTextbox.SendKeys(Faker.Internet.Email());
            }
            Assert.IsTrue(emailForOffersCheckbox.Displayed, "Email for offers checkbox is not displayed on the checkout page.");
            Assert.IsTrue(returnPolicyLink.Displayed, "Return policy link is not displayed on the checkout page.");
        }

        public void VerifyProductPrice()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(shippingMethodsListbox).Perform();
            SelectElement select = new SelectElement(shippingMethodsListbox);
            for (int i = 1; i <= 3; i++)
            {
                select.SelectByValue(i.ToString());
                IWebElement subtotal = _driver.FindElement(By.XPath("//span[@id='cartTotal']"));
                IWebElement shipping = _driver.FindElement(By.XPath("//span[@id='shippingTotal']"));
                IWebElement grandTotal = _driver.FindElement(By.XPath("//span[@id='grandTotal']"));
                string subtotalPrice = subtotal.Text.Trim();
                string shippingPrice = shipping.Text.Trim();
                string grandTotalPrice = grandTotal.Text.Trim();
                subtotalPrice = Regex.Replace(subtotalPrice, @"\s+", " "); // Normalize whitespace
                shippingPrice = Regex.Replace(shippingPrice, @"\s+", " "); // Normalize whitespace
                grandTotalPrice = Regex.Replace(grandTotalPrice, @"\s+", " "); // Normalize whitespace
                subtotalPrice = (subtotalPrice + shippingPrice); // Add dollar sign
                Assert.AreEqual(grandTotalPrice, subtotalPrice, "The product price on the checkout page does not match the expected price.");
            }
        }

        public void FillCreditCardDetails()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(rdButtonCC).Perform();
            rdButtonCC.Click();

            _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-number")));
            string CreditCardNo = SpinlifeStagingConfig.CreditCardNo;
            textBoxCreditCardNo.SendKeys(CreditCardNo);
            _driver.SwitchTo().DefaultContent();

            _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-expirationMonth")));
            dropDownExpiryMonth.SendKeys(DateTime.Now.AddMonths(1).ToString("MM"));
            _driver.SwitchTo().DefaultContent();

            _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-expirationYear")));
            dropDownExpiryYear.SendKeys(DateTime.Now.AddYears(1).ToString("yyyy"));
            _driver.SwitchTo().DefaultContent();

            _driver.SwitchTo().Frame(_driver.FindElement(By.Id("braintree-hosted-field-cvv")));
            textBoxCVV.SendKeys(new Random().Next(100, 999).ToString());
            _driver.SwitchTo().DefaultContent();

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", chkBoxReturnPolicy);
            chkBoxReturnPolicy.Click();
        }

        public void FillPaypalDetails()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(rdButtonPaypal).Perform();
            rdButtonPaypal.Click();
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", chkBoxReturnPolicy);
            chkBoxReturnPolicy.Click();
            // Wait until the iframe is present
            // Scroll into view and click using JavaScript
            // ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center' });", rdButtonPaypal);
            // ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", rdButtonPaypal);
            try
            {
                IWebElement iframe = _driver.FindElement(By.XPath("//iframe[contains(@name, 'paypal')]"));
                _driver.SwitchTo().Frame(iframe);
                btnPaypal.Click();
                Thread.Sleep(2000);
                // _driver.SwitchTo().DefaultContent();
                // WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
                // wait.Until(d => _driver.WindowHandles.Count > 1);
                // _driver.SwitchTo().Window(_driver.WindowHandles[^1]);
                  var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
                string originalWindow = _driver.CurrentWindowHandle;
                wait.Until(d => d.WindowHandles.Count > 1);
                foreach (var handle in _driver.WindowHandles)
                {
                    if (handle != originalWindow)
                    {
                        _driver.SwitchTo().Window(handle);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during PayPal window switch: " + ex.Message);
                throw;
            }

            // IWebElement iframe = _driver.FindElement(By.XPath("//iframe[contains(@name, 'paypal')]"));
            // _driver.SwitchTo().Frame(iframe);
            // btnPaypal.Click();
            // _driver.SwitchTo().DefaultContent();
            // WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            // wait.Until(d => _driver.WindowHandles.Count > 1);
            // _driver.SwitchTo().Window(_driver.WindowHandles[^1]);
            textboxPaypalEmail.SendKeys("deepa.oberoi@spinlife.com");
            btnNext.Click();
            textboxPaypalPassword.SendKeys("ItIsFoggyInIndia");
            btnLogin.Click();
            Extensions.WaitForVisible(btnPaypalCompletePurchase, 10000);
            btnPaypalCompletePurchase.Click();
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
        }
        public void CheckOrMoneyOrder()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(rdoCheckOrMoneyOrder).Perform();
            rdoCheckOrMoneyOrder.Click();
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", chkBoxReturnPolicy);
            chkBoxReturnPolicy.Click();
            btnPlaceOrder.Click();
            Thread.Sleep(2000);
        }

        public void FillBreadPayDetails()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(rdButtonBreadPay).Perform();
            rdButtonBreadPay.Click();
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", chkBoxReturnPolicy);
            chkBoxReturnPolicy.Click();
            //Extensions.WaitForEnabled(btnPlaceOrder, 5000);
            Thread.Sleep(2000);
            btnPlaceOrder.Click();

            IWebElement iframe = _driver.FindElement(By.XPath("//iframe[contains(@name, '__zoid__checkout_component__')]"));
            _driver.SwitchTo().Frame(iframe);
            Extensions.WaitForVisible(btnAcceptCookies, 10000);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", btnAcceptCookies);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnGetStarted);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", btnGetStarted);
            textBoxBreadPayEmail.SendKeys("deepa.oberoi@centricconsulting.com");
            btnBreadyPayContinue.Click();
            textBoxBreadPayPhone.SendKeys(Faker.Phone.Number());
            btnBreadyPaySubmit.Click();
            Extensions.WaitForVisible(textboxBreadyPayOTPCode, 10000);
            textboxBreadyPayOTPCode.SendKeys("1234");
            Extensions.WaitForVisible(enterAddressManually, 10000);
            enterAddressManually.Click();
            textboxBreadyPayFirstName.SendKeys(Faker.Name.First());
            textboxBreadyPayLastName.SendKeys(Faker.Name.Last());
            textboxBreadyPayAddress.SendKeys(Faker.Address.StreetAddress());
            zipCode.SendKeys("30301");
            Thread.Sleep(2000); // Wait for the address suggestions to load
            // textboxBreadyPayAddress.SendKeys(Keys.Down); // Press Down Arrow to select the first suggestion
            // textboxBreadyPayAddress.SendKeys(Keys.Enter); // Press Enter to confirm the selection
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => cityLocalityTextbox.Displayed);
            chkboxBreadyPay.Click();
            btnBreadPayContinue.Click();
            Random random = new Random();

            // Define the start and end dates
            DateTime startDate = new DateTime(1950, 1, 1); // Earliest possible date
            DateTime endDate = new DateTime(1999, 12, 31); // Latest possible date

            // Generate a random number of days between startDate and endDate
            int range = (endDate - startDate).Days;
            DateTime birthdate = startDate.AddDays(random.Next(range));

            // Format as MM dd yyyy
            string formattedDate = birthdate.ToString("MM dd yyyy");
            Extensions.WaitForVisible(textboxBreadyPayBirtDate, 10000);
            textboxBreadyPayBirtDate.SendKeys(formattedDate);

            // Generate a random two-digit number (10 to 99)
            int twoDigitNumber = random.Next(10, 100);

            // Concatenate the fixed and random parts
            string result = $"1234507{twoDigitNumber}";
            textboxBreadyPaySSN.SendKeys(result);
            btnBreadPayViewOffers.Click();
            //if (labelBreadPaySSNError.Displayed)
            Thread.Sleep(1000);
            while (true) // Infinite loop, will break when error disappears
            {
                var errorElements = _driver.FindElements(By.XPath("//div[@data-testid='form-submit-error']//div[@role='status']"));

                if (errorElements.Count > 0 && errorElements[0].Displayed)
                {
                    int twoDigitNumber1 = random.Next(10, 100);
                    string result1 = $"1234507{twoDigitNumber1}";
                    textboxBreadyPaySSN.Clear();
                    textboxBreadyPaySSN.SendKeys(result1);
                    btnBreadPayViewOffers.Click();
                    Thread.Sleep(500);
                }
                else
                {
                    // Error is gone, exit the loop
                    break;
                }

                Thread.Sleep(500); // Add a small delay to avoid excessive CPU usage
            }

            // Now click the EMI option
            Extensions.WaitForVisible(rdButtonEMIOption, 10000);
            rdButtonEMIOption.Click();

            Thread.Sleep(2000);
            btnContinueToReview.Click();

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", chkBoxBreadPayDisclosureAcceptance);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", chkBoxBreadPayDisclosureAcceptance);
            Thread.Sleep(2000);
            //chkBoxBreadPayDisclosureAcceptance.Click();
            btnBreadPayPlaceOrder.Click();
            Thread.Sleep(15000);

        }
        public string VerifyCheckoutPrice()
        {
            // Wait for the checkout price element to be visible
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement checkoutPriceElement = wait.Until(d => d.FindElement(By.XPath("//span[@id='cartTotal']")));

            // Get the text of the checkout price element
            string checkoutPriceText = "$" + checkoutPriceElement.Text;
            return checkoutPriceText;
        }

        public void SelectProductQuantity(string quantity)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement quantityTextbox = wait.Until(d => d.FindElement(By.XPath("//input[@name='itemQty']")));

            // Robust clear
            quantityTextbox.Click();
            quantityTextbox.SendKeys(Keys.Control + "a");
            quantityTextbox.SendKeys(Keys.Delete);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '';", quantityTextbox);

            Extensions.WaitForVisible(quantityTextbox, 10000);
            quantityTextbox.SendKeys(quantity);
            updateButtonLink.Click();
            Extensions.WaitForVisible(updateButtonLink, 10000);
        }

        public void SelectMailaCheckOrMoneyOrder()
        {
            rdButtonChequeOrMoneyOrder.Click();
        }
        public void ClickOnPlaceOrder()
        {
            btnPlaceOrder.Click();
            //Extensions.WaitForVisible(labelOrderConfirmation, 10000);
        }

        public bool IsOrderConfirmationDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Thread.Sleep(4000);
            if (closeButton.Displayed)
            {
                Extensions.WaitForVisible(closeButton, 10000);
                closeButton.Click();
            }
            else
            {
                Console.WriteLine("Close button is not displayed.");
            }
            try
            {
                // Wait for either confirmation label to be visible
                return wait.Until(driver =>
                {
                    var label1 = driver.FindElements(By.XPath("//h1[contains(text(), 'Thank you')]"));

                    // Wait for popup to be visible before checking label2
                    var popup = driver.FindElements(By.XPath("//div[contains(@class, 'popup') and contains(@class, 'visible')]"));
                    if (popup.Count > 0)
                    {
                        var label2 = driver.FindElements(By.XPath("//p[@class='title' and normalize-space(text())='THANK YOU FOR YOUR PURCHASE!']"));
                        return (label2.Count > 0 && label2[0].Displayed);
                    }

                    return (label1.Count > 0 && label1[0].Displayed);
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false; // Order confirmation was not displayed in time
            }
        }


        public void BetteLogin()
        {
            textboxBetteEmail.SendKeys(SpinlifeStagingConfig.BetteUserName);
            textboxBettePassword.SendKeys(SpinlifeStagingConfig.BettePassword);
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
        private IWebElement rdButtonPaypal => _driver.FindElement(By.CssSelector("label[for='payment_type2']"));
        private IWebElement payTomorrow => _driver.FindElement(By.XPath("//label[@for='payment_type35']"));
        private IWebElement rdButtonChequeOrMoneyOrder => _driver.FindElement(By.CssSelector("label[for='payment_type6']"));
        private IWebElement rdButtonBreadPay => _driver.FindElement(By.CssSelector("label[for='payment_type10']"));
        private IWebElement textBoxCreditCardNo => _driver.FindElement(By.XPath("//*[@id='credit-card-number']"));

        private IWebElement dropDownExpiryYear => _driver.FindElement(By.XPath("//*[@id='expiration-year']"));
        private IWebElement dropDownExpiryMonth => _driver.FindElement(By.XPath("//*[@id='expiration-month']"));
        private IWebElement textBoxCardFirstName => _driver.FindElement(By.XPath("//*[@id='card_first_name']"));
        private IWebElement textBoxCardLastName => _driver.FindElement(By.XPath("//*[@id='card_last_name']"));
        private IWebElement textBoxCVV => _driver.FindElement(By.XPath("//*[@id='cvv']"));
        private IWebElement chkBoxReturnPolicy => _driver.FindElement(By.Id("feReturnPolicy"));
        private IWebElement btnPlaceOrder => _driver.FindElement(By.XPath("//*[@id='submit-button']"));
        private IWebElement btnPaypal => _driver.FindElement(By.XPath("//div[@role='link' and @data-funding-source='paypal']"));
        private IWebElement labelOrderConfirmation => _driver.FindElement(By.XPath("(//h1[contains(text(), 'Thank you')])"));
        private IWebElement labelOrderConfirmationPopUp => _driver.FindElement(By.XPath("(//p[@class='title' and text()='THANK YOU FOR YOUR PURCHASE!']])"));
        private IWebElement textboxBetteEmail => _driver.FindElement(By.XPath("//*[@id='Username']"));
        private IWebElement textboxBettePassword => _driver.FindElement(By.XPath("//*[@id='Password']"));
        private IWebElement btnBetteLogin => _driver.FindElement(By.XPath("//*[@id='loginButton']"));
        private IWebElement textboxPaypalEmail => _driver.FindElement(By.XPath("//*[@id='email']"));
        private IWebElement btnNext => _driver.FindElement(By.XPath("//*[@id='btnNext']"));
        private IWebElement textboxPaypalPassword => _driver.FindElement(By.XPath("//*[@id='password']"));
        private IWebElement btnLogin => _driver.FindElement(By.XPath("//*[@id='btnLogin']"));
        private IWebElement btnPaypalSubmit => _driver.FindElement(By.XPath("//*[@id='payment-submit-btn']"));
        private IWebElement btnPaypalCompletePurchase => _driver.FindElement(By.XPath("//*[text()='Complete Purchase']"));
        private IWebElement btnGetStarted => _driver.FindElement(By.XPath("//button[div[contains(text(), 'Get started')]]"));
        private IWebElement textBoxBreadPayEmail => _driver.FindElement(By.XPath("//input[@id='email' and @name='email' and @type='text']"));
        private IWebElement btnBreadyPayContinue => _driver.FindElement(By.XPath("//button[@type='submit' and div[contains(text(), 'Continue')]]"));
        private IWebElement textBoxBreadPayPhone => _driver.FindElement(By.XPath("//input[@id='phone']"));
        private IWebElement btnBreadyPaySubmit => _driver.FindElement(By.XPath("//button[@type='submit' and div[contains(text(), 'Submit')]]"));
        private IWebElement textboxBreadyPayOTPCode => _driver.FindElement(By.XPath("//input[@id='otpCode']"));
        private IWebElement btnAcceptCookies => _driver.FindElement(By.XPath("//*[@id='onetrust-accept-btn-handler']"));
        private IWebElement textboxBreadyPayFirstName => _driver.FindElement(By.XPath("//input[@id='givenName']"));
        private IWebElement textboxBreadyPayLastName => _driver.FindElement(By.XPath("//input[@id='familyName']"));
        private IWebElement textboxBreadyPayAddress => _driver.FindElement(By.XPath("//input[@id='address1']"));
        private IWebElement zipCode => _driver.FindElement(By.XPath("//input[@id='postalCode']"));
        private IWebElement textboxBreadyPayBirtDate => _driver.FindElement(By.XPath("//input[@id='birthDate']"));
        private IWebElement textboxBreadyPaySSN => _driver.FindElement(By.XPath("//input[@id='fullIIN']"));
        private IWebElement btnBreadPayViewOffers => _driver.FindElement(By.XPath("//button[normalize-space(.)='View your offers']"));
        private IWebElement chkboxBreadyPay => _driver.FindElement(By.XPath("//div[contains(@class, 'checkbox-box')]"));
        private IWebElement enterAddressManually => _driver.FindElement(By.XPath("//button[text()='Enter address manually']"));
        private IWebElement btnBreadPayContinue => _driver.FindElement(By.XPath("//button[div[text()='Continue']]"));
        private IWebElement rdButtonEMIOption => _driver.FindElement(By.XPath("//div[text()='3 months']"));

        private IWebElement btnContinueToReview => _driver.FindElement(By.XPath("//button[div[text()='Continue to review']]"));

        private IWebElement chkBoxBreadPayDisclosureAcceptance => _driver.FindElement(By.XPath("//input[@id='disclosureAcceptance']"));
        private IWebElement btnBreadPayPlaceOrder => _driver.FindElement(By.XPath("//button/div[text()='Place order']"));
        private IWebElement labelBreadPaySSNError => _driver.FindElement(By.XPath("//div[@data-testid='form-submit-error']//div[@role='status']"));
        private IWebElement closeButton => _driver.FindElement(By.XPath("//button[@aria-label='Close']"));
        private IWebElement updateButtonLink => _driver.FindElement(By.XPath("//a[@class='update-link']"));
        private IWebElement cityLocalityTextbox => _driver.FindElement(By.XPath("//input[@id='locality']"));
        private IWebElement dropDownFeatherChairColor => _driver.FindElement(By.XPath("//div[@id='s_X2252']"));
        private IWebElement dropDownFeatherChairColorValue => _driver.FindElement(By.XPath("//p[text()='Grey/Orange Cushion/Back Overlay']"));
        private IWebElement dropDownFeatherChairLegrest => _driver.FindElement(By.XPath("//div[@id='s_X256']"));
        private IWebElement dropDownFeatherChairLegrestValue => _driver.FindElement(By.XPath("//p[text()='Swingaway Legrests']"));
        private IWebElement emailTextbox => _driver.FindElement(By.XPath("//input[@id='email']"));
        private IWebElement returnPolicyLink => _driver.FindElement(By.XPath("//a[@alt='View full return on this category.']"));
        private IWebElement updateAddressCheckbox => _driver.FindElement(By.XPath("//input[@id='updateAccountAddress']"));
        private IWebElement emailForOffersCheckbox => _driver.FindElement(By.XPath("//input[@id='email_opt']"));
        private IWebElement shippingMethodsListbox => _driver.FindElement(By.XPath("//select[@id='shipping_flag_8322808']"));
        private IWebElement dropdownViper => _driver.FindElement(By.XPath("//div[@id='s_X110']"));
        private IWebElement dropdownViperValue => _driver.FindElement(By.XPath("//div[@id='i_X108726']"));

        private IWebElement dropdownViperLegrests => _driver.FindElement(By.XPath("//div[@id='s_X256']"));
        private IWebElement dropdownViperLegrestsValue => _driver.FindElement(By.XPath("//div[@id='i_X108729']"));
        private IWebElement rdoCheckOrMoneyOrder => _driver.FindElement(By.XPath("//span[text()='Mail a Check or Money Order']"));
    }
}