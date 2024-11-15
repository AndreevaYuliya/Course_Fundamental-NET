Feature: Login

  Scenario Outline: User Login with various usernames
    Given I am on the login page
    When I login with username "<username>" and password "secret_sauce"
    Then I should see the appropriate result for username "<username>"

    Examples:
      | username               |
      | standard_user          |
      | locked_out_user        |
      | problem_user           |
      | performance_glitch_user|
      | error_user             |
      | visual_user            |
