Feature: Spinlife
		As a User,
		I want to purchase a product without logging in

@uiTest @deepa
Scenario: Single Page Checkout
	Given I am on Spinlife Staging Home Page
	When I search for "SpinLife Classic PR-458 3-Position"
	And I click on View Product
	Then I should see the product details
	#When I click on Add to Cart
	#Then I should see mandatory validation for SpinLife Classic PR-458 3-Position
	When I fill mandatory details for SpinLife Classic PR-458 3-Position
	And I click on Add to Cart
    Then I should see the product added to cart
	When I click on View Cart
	And I click on Proceed to Checkout
	And I fill the shipping details
	And I fill Credit Card details
	And I click on Place Order
	Then I should see the order confirmation
