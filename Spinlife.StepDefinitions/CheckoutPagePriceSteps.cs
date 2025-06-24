using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using Spinlife.Config;
using Spinlife.PageObjects;
using TechTalk.SpecFlow; 


namespace Spinlife.StepDefinitions
{
	[Binding]
	[DebuggerStepThrough]
	public class CheckoutPagePriceSteps
	{
		private readonly CheckoutPagePrice _page;
		public CheckoutPagePriceSteps(IWebDriver driver)
		{
			_page = new CheckoutPagePrice(driver);
		}

		[Given(@"I am on Spinlife Staging Home Page")]
		public void WhenIAmOnSpinlifeStagingHomePage()
		{
			_page.Navigate(SpinlifeStagingConfig.stagingurl);
		}

		[When(@"I search for product ""(.*)""")]
		public void WhenISearchFor(string product)
		{
			_page.SearchForProduct(product);
		}

		[When(@"I click on View Product")]
		public void WhenIClickOnViewProduct()
		{
			_page.ClickOnViewProduct();
		}

		[Then(@"I should see the product details")]
		public void ThenIShouldSeeTheProductDetails()
		{
			Assert.True(_page.ViewProductDetails());
		}

		[When(@"I fill mandatory details for ""(.*)""")]
		public void WhenIFillMandatoryFieldsForProduct(string productName)
		{
			if(productName.Equals("SpinLife Classic PR-458 3-Position", StringComparison.OrdinalIgnoreCase))
			{
				_page.SelectUpholstery();
			}
			else if (productName.Equals("Feather Feather Chair  - Feather Standard  Lightweight", StringComparison.OrdinalIgnoreCase))
			{
				_page.SelectDropdownValues();
			}
			else if (productName.Equals("Feather Feather Scooter  - Feather 4-Wheel Travel Scooters", StringComparison.OrdinalIgnoreCase))
			{
				Assert.True(_page.SelectFeatherScooter());
			}
			else if (productName.Equals("Drive Medical Viper Plus GT", StringComparison.OrdinalIgnoreCase))
			{
				_page.SelectViperScooter();
			}
			else
			{
				throw new NotImplementedException($"Product '{productName}' is not implemented.");
			}
		}

        
		[When(@"I click on Add to Cart")]
		public void WhenIClickOnAddToCart()
		{
			_page.ClickOnAddToCart();
		}

		[Then(@"I should see the ""(.*)"" added to cart")]
		public void ThenIShouldSeeTheProductAddedToCart(string productName)
		{
			if (productName.Equals("SpinLife Classic PR-458 3-Position", StringComparison.OrdinalIgnoreCase))
			{
				Assert.True(_page.IsProductAddedToCart());
			}
			else if (productName.Equals("Feather Feather Chair  - Feather Standard  Lightweight", StringComparison.OrdinalIgnoreCase))
			{
				Assert.True(_page.IsProductAddedToCart());
			}
			else if (productName.Equals("Feather Feather Scooter  - Feather 4-Wheel Travel Scooters", StringComparison.OrdinalIgnoreCase))
			{
				Assert.True(_page.IsProductAddedToCart());
			}
			else if (productName.Equals("Drive Medical Viper Plus GT", StringComparison.OrdinalIgnoreCase))
			{
				Assert.True(_page.IsProductAddedToCart());
			}
			else
			{
				throw new NotImplementedException($"Product '{productName}' is not implemented.");
			}
		}

		[When(@"I click on View Cart")]
		public void WhenIClickOnViewCart()
		{
			_page.ClickOnViewCart();
		}

        [When(@"I select the product quantity as ""(.*)""")]
        public void WhenISelectTheProductQuantityAs(string quantity)
        {
            _page.SelectProductQuantity(quantity);
        }

		[When(@"I click on proceed to checkout")]
		public void WhenIClickOnProceedToCheckout()
		{
			_page.ClickOnProceedToCheckout(); 
		}

		[Then(@"I should see the ""(.*)"" price displayed on the checkout page ""(.*)""")]
		public void ThenIShouldSeeThePriceDisplayedOnCheckoutPage(string expectedPrice, string productName)
		{
			Assert.AreEqual(expectedPrice, _page.VerifyCheckoutPrice());
			if (productName.Equals("Drive Medical Viper Plus GT", StringComparison.OrdinalIgnoreCase))
			{
				_page.VerifyProductPrice();
				Console.WriteLine("Price displayed on checkout page is correct for selected shipping.");
				_page.VerifyCheckoutPage();
			}
		}

		[When(@"I fill the shipping details")]
		public void WhenIFillTheShippingDetails()
		{
			_page.FillShippingDetails();
		}

		[When(@"I fill in ""(.*)"" details")]
		public void WhenIFillPaymentDetails(string paymentOption)
		{
			if (paymentOption.Equals("Credit Card", StringComparison.OrdinalIgnoreCase))
			{
				_page.FillCreditCardDetails();
			}
			else if (paymentOption.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
			{
				_page.FillPaypalDetails();
			}
			else if (paymentOption.Equals("BreadPay", StringComparison.OrdinalIgnoreCase))
			{
				_page.FillBreadPayDetails();
			}
			else if (paymentOption.Equals("Check or Money Order", StringComparison.OrdinalIgnoreCase))
			{
				_page.CheckOrMoneyOrder();
			}
			else
			{
				throw new NotImplementedException($"Payment option '{paymentOption}' is not implemented.");
			}
		}


		[When(@"I click on Place Order for ""(.*)""")]
		public void WhenIClickOnPlaceOrder(string paymentOption)
		{
			if (paymentOption.Equals("Credit Card", StringComparison.OrdinalIgnoreCase))
			{
				_page.ClickOnPlaceOrder();
			}
		}

		[Then(@"I should see the order confirmation")]
		public void ThenIShouldSeeTheOrderConfirmation()
		{
			Assert.True(_page.IsOrderConfirmationDisplayed());
		}

	}
} 