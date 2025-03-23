# E2ETestAutomation

A .NET Playwright end-to-end test automation project designed to test a sample web application. The solution includes:

- **Playwright** for browser automation.  
- **NUnit** for the testing framework.  
- **Utilities** for configuration, database helpers, and test data.  
- **Pages** for Page Object Model classes.  
- **Tests** for individual test cases.

---

## Table of Contents

1. [Prerequisites](#prerequisites)  
2. [Project Structure](#project-structure)  
3. [Installation](#installation)  
4. [Configuration](#configuration)  
5. [Running Tests](#running-tests)  
6. [Generating Test Reports](#generating-test-reports)  
---

## Prerequisites

- **.NET SDK 6.0 or later**

---

## Project Structure
```
E2ETestAutomation/
│
├─ bin/                      # Build output (ignored by .gitignore)
├─ config/                   # Configuration files (e.g., appsettings.json, ConfigReader.cs)
├─ DummyData/
│   └─ DummyData.json        # JSON test data (if still using this folder)
├─ Pages/
│   ├─ IAddToCartPage.cs     # Interface for add-to-cart page object
│   ├─ ILoginPage.cs         # Interface for login page object
│   ├─ AddToCartPage.cs      # Page object for add-to-cart flows
│   ├─ LoginPage.cs          # Page object for login flows
│   └─ …
├─ Reports/                  # Test reports output folder (ignored by .gitignore)
├─ Tests/
│   ├─ PageTest.cs           # Base test class for Playwright (launch config, etc.)
│   ├─ LoginTests.cs         # Example test for login flows
│   ├─ AddToCardPageWithDatabaseTest.cs # Example test using DB checks
│   └─ …
├─ Utilities/
│   ├─ DatabaseHelper.cs     # DB connection logic (MySQL or SQLite, etc.)
│   ├─ TestData.cs           # Model and helper for loading test data from JSON
│   └─ ConfigReader.cs       # Reads configuration from appsettings.json
├─ .gitignore
├─ E2ETestAutomation.csproj
├─ README.md                 # Project documentation
└─ appsettings.json          # Configuration file (copied to bin folder on build)
```
---

## Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/adnansid/E2ETestAutomation.git
   cd E2ETestAutomation

2.	Install Dependencies:

dotnet restore

This installs all NuGet packages (including Playwright, NUnit, and configuration packages).

3.	(Optional) Install Playwright Browsers:
If you need to install or update the browsers used by Playwright:

npx playwright install

or

dotnet tool install --global Microsoft.Playwright.CLI
playwright install



Configuration
	•	appsettings.json
Contains settings like the base URL, browser launch configurations, and database connection strings.
Example snippet:

{
  "BaseUrl": "http://www.automationpractice.pl",
  "BrowserConfig": {
    "Headless": "false",
    "Args": [ "--start-maximized" ]
  },
  "ConnectionStrings": {
    "MySqlConnection": "Server=localhost;Database=imaginary_db;Uid=testuser;Pwd=testpass;"
  }
}


	•	ConfigReader.cs
A utility class (in the Utilities/ folder) that reads values from appsettings.json.

Ensure that appsettings.json is copied to the output directory. If using Visual Studio, set the file’s properties to Copy if newer. Otherwise, add the following to your .csproj:

<ItemGroup>
  <None Update="config/appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>




Running Tests
1.	Build the Project:

dotnet build


2.	Run All Tests:

dotnet test

Or run a specific test/filter:

dotnet test --filter "TestNameOrCategory"


3.	Example:

dotnet test --filter "AddToCartWithDatabase"



Playwright will launch the browser based on the settings in PageTest.cs and appsettings.json.


Generating Test Reports
1.	Generate a TRX Report:

dotnet test --logger "trx;LogFileName=TestResults.trx"


2.	Convert TRX to HTML using ReportGenerator:
	•	Install the tool if not already installed:

dotnet tool install -g dotnet-reportgenerator-globaltool


	•	Generate TRX report:

dotnet test --logger "trx;LogFileName=TestResults.trx"


	•	Convert to HTML:

reportgenerator -reports:TestResults.trx -targetdir:TestReport -reporttypes:Html


	•	Open TestReport/index.html in your browser.


Feel free to reach out if you have any questions or run into issues.