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
	public class CartPageFunctionSteps
	{
		private readonly CartPageFunction _page;
		public CartPageFunctionSteps(IWebDriver driver)
		{
			_page = new CartPageFunction(driver);
		}

		[Then(@"I should see the product price as ""(.*)"" for ""(.*)""")]
		public void ThenIShouldSeeTheProductPriceAs(string expectedPrice, string quantity)
		{
			_page.verifyProductPriceCorrect(expectedPrice, quantity);
		}

		[When(@"I apply the coupon code")]
		public void WhenIApplyTheCouponCode()
		{
			_page.verifyCouponCode("POP10");
		}

		[Then(@"I should see the discount applied to the cart total for ""(.*)""")]
		public void ThenIShouldSeeTheDiscountAppliedToTheCartTotal(string quantity)
		{
			Assert.True(_page.verifyDiscountAppliedToCartTotal(quantity));
		}
	}
}	