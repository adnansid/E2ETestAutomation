using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using E2ETestAutomation.Pages;
using E2ETestAutomation.Pages.Interfaces;
using E2ETestAutomation.Utilities; // Updated namespace for TestData and TestDataHelper
using System.IO;
using System.Threading.Tasks;

namespace E2ETestAutomation.Tests
{
    [TestFixture]
    public class LoginTests : PageTest
    {
        private ILoginPage _loginPage;
        private TestData[] _testData;

        [SetUp]
        public async Task SetupAsync()
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "DummyData", "DummyData.json");

            _testData = await TestDataHelper.LoadTestDataAsync(jsonPath);

            if (_testData == null || _testData.Length == 0)
            {
                throw new InvalidOperationException("No test data loaded.");
            }

            TestContext.WriteLine($"Loaded test data: Username={_testData[0].Username}, Password={_testData[0].Password}");

            _loginPage = new LoginPage(Page);
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            // Navigate using the BaseUrl read from config in PageTest.
            await _loginPage.GotoLoginPage();

            // Perform login action using credentials from test data.
            await _loginPage.LoginAction(_testData[0].Username, _testData[0].Password);
            TestContext.WriteLine($"Logging in with credentials: {_testData[0].Username} {_testData[0].Password}");

            // Assert that the login assertion element is visible.
            await Expect(_loginPage.LoginAssertion).ToBeVisibleAsync();
        }
    }
}