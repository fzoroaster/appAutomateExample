@browserstack
Feature: Sample

    Scenario: Open BrowserStack driver
        Given BrowserStack driver running
        When Allow all system permission
        Then The application is loaded