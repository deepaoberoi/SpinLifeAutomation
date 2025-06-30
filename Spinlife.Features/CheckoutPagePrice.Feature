Feature: Checkout Page Price Validation for Selected Product Quantity

@uiTest @aniketb
Scenario: Validate the price displayed on the checkout page for selected quantity
   
    Given I am on Spinlife Staging Home Page
	When I search for product "<ProductName>"
	And I click on View Product
	Then I should see the product details
    When I fill mandatory details for "<ProductName>"
	And I click on Add to Cart
    Then I should see the "<ProductName>" added to cart
	When I click on View Cart
    And I select the product quantity as "<Quantity>" 
    When I click on proceed to checkout
    Then I should see the "<ExpectedPrice>" price displayed on the checkout page "<ProductName>"
    When I fill the shipping details
	When I fill in "<PaymentOption>" details
	When I click on Place Order for "<PaymentOption>"
	Then I should see the order confirmation

Examples:
    | ProductName                        | Quantity | ExpectedPrice | PaymentOption |

       | SpinLife Classic PR-458 3-Position | 1        | $699.00       | Credit Card           |
       | SpinLife Classic PR-458 3-Position | 2        | $1,398.00     | Check or Money Order  |
       | SpinLife Classic PR-458 3-Position | 3        | $2,097.00     | Check or Money Order  |
    #    | SpinLife Classic PR-458 3-Position | 4        | $2,796.00     | BreadPay              |
    #    | SpinLife Classic PR-458 3-Position | 5        | $3,495.00     | PayTomorrow           |

       | Feather Feather Chair  - Feather Standard  Lightweight | 1        | $649.00     | Credit Card            |
       | Feather Feather Chair  - Feather Standard  Lightweight | 2        | $1,298.00   | Check or Money Order   |
       | Feather Feather Chair  - Feather Standard  Lightweight | 3        | $1,947.00   | Credit Card            |
    #    | Feather Feather Chair  - Feather Standard  Lightweight | 4        | $2,596.00   | BreadPay               |
    #    | Feather Feather Chair  - Feather Standard  Lightweight | 5        | $3,245.00   | PayTomorrow            |

       |   Drive Medical Viper Plus GT            | 1        | $406.00     | Credit Card   |
       |   Drive Medical Viper Plus GT            | 2        | $812.00     | Check or Money Order   |
       |   Drive Medical Viper Plus GT            | 3        | $1,218.00   | Check or Money Order   |
       |   Drive Medical Viper Plus GT            | 4        | $1,624.00   | Credit Card   |
    #    |   Drive Medical Viper Plus GT            | 5        | $2,030.00   | PayTomorrow   |

@uiTest @aniketb
Scenario: Validate the cart page functionality
   
    Given I am on Spinlife Staging Home Page
	When I search for product "<ProductName>"
	And I click on View Product
	Then I should see the product details
    When I fill mandatory details for "<ProductName>"
	And I click on Add to Cart
    Then I should see the "<ProductName>" added to cart
	When I click on View Cart
    And I select the product quantity as "<Quantity>" 
	Then I should see the product price as "<ExpectedPrice>" for "<Quantity>"
	When I apply the coupon code
	Then I should see the discount applied to the cart total for "<Quantity>"
    When I remove the product from the cart
    Then I should see the cart is empty

Examples:

    | ProductName                                               | Quantity | ExpectedPrice |
    
    | Pride Travel Pro 3-Wheel  - Pride 3-Wheel Travel Scooters | 1        | $699.00       |
    | Pride Travel Pro 3-Wheel  - Pride 3-Wheel Travel Scooters | 2        | $1,398.00     |
    | Pride Travel Pro 3-Wheel  - Pride 3-Wheel Travel Scooters | 3        | $2,097.00     |
    | Pride Travel Pro 3-Wheel  - Pride 3-Wheel Travel Scooters | 4        | $2,796.00     |
    | Pride Travel Pro 3-Wheel  - Pride 3-Wheel Travel Scooters | 5        | $3,495.00     |

    | Pride Go Go Sport 3-Wheel  - Pride 3-Wheel Travel Scooters | 1        | $1,549.00     |
    | Pride Go Go Sport 3-Wheel  - Pride 3-Wheel Travel Scooters | 2        | $3,098.00     |
    | Pride Go Go Sport 3-Wheel  - Pride 3-Wheel Travel Scooters | 3        | $4,647.00     |
    | Pride Go Go Sport 3-Wheel  - Pride 3-Wheel Travel Scooters | 4        | $6,196.00     |
    | Pride Go Go Sport 3-Wheel  - Pride 3-Wheel Travel Scooters | 5        | $7,745.00     |

    | Feather Feather Scooter  - Feather 4-Wheel Travel Scooters | 1       | $1,999.00     | 
    | Feather Feather Scooter  - Feather 4-Wheel Travel Scooters | 2       | $3,998.00     | 
    | Feather Feather Scooter  - Feather 4-Wheel Travel Scooters | 3       | $5,997.00     | 
    | Feather Feather Scooter  - Feather 4-Wheel Travel Scooters | 4       | $7,996.00     | 
    | Feather Feather Scooter  - Feather 4-Wheel Travel Scooters | 5       | $9,995.00     |

    # | Drive Medical Heavy Duty Transport Chair                  | 1        | $288.00       |
