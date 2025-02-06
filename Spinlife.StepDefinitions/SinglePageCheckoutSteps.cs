using System.Runtime.CompilerServices;
using System.Threading;
using AlerStallings.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AlerStallings.StepDefinitions
{
	[Binding]
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
			string username = "webqa";
			string password = "Rev0luti0ns";
			string managemycookieurl = $"https://{username}:{password}@staging.spinlife.biz/managemycookies.cfm?gfs=1&action=set&cookie_id=51&new_value=on";
			
			string url = $"https://{username}:{password}@staging.spinlife.biz";

			// Navigate to the URL
			_page.Navigate(managemycookieurl);
	
_page.BetteLogin();
_page.Navigate(managemycookieurl);
//_page.Navigate(url);
			//Assert HomePage is displayed
			//Assert.True(_page.IsHomePageDisplayed());

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

		[When (@"I fill the shipping details")]
		public void WhenIFillTheShippingDetails()
		{
			_page.FillShippingDetails();
		}

		[When (@"I fill Credit Card details")]

		public void WhenIFillCreditCardDetails()
		{
			_page.FillCreditCardDetails();
		}

		// public void WhenIFillCreditCardDetails()
		// {
		//  	_page.SelectMailaCheckOrMoneyOrder();
		// } 
		
		[When (@"I click on Place Order")]
		public void WhenIClickOnPlaceOrder()
		{
			_page.ClickOnPlaceOrder();
		}

		[Then(@"I should see the order confirmation")]
		public void ThenIShouldSeeTheOrderConfirmation()
		{
			Assert.True(_page.IsOrderConfirmationDisplayed());
		}

	}
}
