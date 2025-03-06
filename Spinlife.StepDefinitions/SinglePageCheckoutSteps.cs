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
	public class SinglePageCheckoutSteps
	{
		private readonly SinglePageCheckout _page;
		public SinglePageCheckoutSteps(IWebDriver driver)
		{
			_page = new SinglePageCheckout(driver);
		}

		[Given(@"I am on Spinlife Staging Home Page")]
		public void WhenIAmOnSpinlifeStagingHomePage()
		{
			//Navigate to Bette Url and Login
			//_page.Navigate(SpinlifeStagingConfig.BetteUrl);
			//_page.BetteLogin();

			//Set SiglePageCheckoutCookie to On
			//_page.Navigate(SpinlifeStagingConfig.managemycookieurl);
			_page.Navigate(SpinlifeStagingConfig.stagingurl);


		}

		[When(@"I search for ""(.*)""")]
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

		[When(@"I click on Add to Cart")]
		public void WhenIClickOnAddToCart()
		{
			_page.ClickOnAddToCart();
		}

		[Then(@"I should see mandatory validation for SpinLife Classic PR-458 3-Position")]
		public void ThenIShouldSeeMandatoryValidationForSpinLifeClassicPR_Position()
		{
			Assert.True(_page.IsMandatoryValidationDisplayed());
		}

		[When(@"I fill mandatory details for SpinLife Classic PR-458 3-Position")]
		public void WhenIFillMandatoryFieldsForSpinLifeClassicPR_Position()
		{
			_page.SelectUpholstery();
		}

		[Then(@"I should see the product added to cart")]
		public void ThenIShouldSeeTheProductAddedToCart()
		{
			Assert.True(_page.IsProductAddedToCart());
		}

		[When(@"I click on View Cart")]
		public void WhenIClickOnViewCart()
		{
			_page.ClickOnViewCart();
		}

		[When(@"I click on Proceed to Checkout")]
		public void WhenIClickOnProceedToCheckout()
		{
			_page.ClickOnProceedToCheckout();
		}

		[When(@"I fill the shipping details")]
		public void WhenIFillTheShippingDetails()
		{
			_page.FillShippingDetails();
		}

		// [When(@"I fill Credit Card details")]

		// public void WhenIFillCreditCardDetails()
		// {
		// 	_page.FillCreditCardDetails();
		// }

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
