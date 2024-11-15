Feature: Login functionality

  Scenario: Login with valid credentials
    Given I am on the login page
    When I attempt to login with valid credentials
    Then Wait for the page to load and check the page title

  Scenario: Login with only username
    Given I am on the login page
    When I attempt to login with only username
    Then Check for error message or correct page

  Scenario: Login with empty credentials
    Given I am on the login page
    When I attempt to login with empty credentials
    Then Check for error message or correct page after empty credentials
