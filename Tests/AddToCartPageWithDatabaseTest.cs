using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using E2ETestAutomation.Pages;
using System.IO;
using System.Threading.Tasks;
using E2ETestAutomation.Pages.Interfaces;
using E2ETestAutomation.Utilities; // <-- for DatabaseHelper
using System;

namespace E2ETestAutomation.Tests
{
    public class AddToCartPageWithDatabaseTest : PageTest
    {
        private ILoginPage _loginPage;
        private AddToCartPage _addToCartPage;
        private TestData[] _testData;

        [SetUp]
        public async Task SetupAsync()
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "DummyData", "DummyData.json");
            _testData = await TestDataHelper.LoadTestDataAsync(jsonPath);

            _loginPage = new LoginPage(Page);
            _addToCartPage = new AddToCartPage(Page);
        }

        [Test]
        [Timeout(60000)]
        public async Task AddToCartWithDatabase()
        {
            // --- Application steps ---
            // Login
            await _loginPage.GotoLoginPage();
            await _loginPage.LoginAction(_testData[1].Username, _testData[1].Password);

            // Navigate and perform cart operations
            await _addToCartPage.NavigateToDressTab();
            await _addToCartPage.SelectBlouse();            
            await _addToCartPage.AddWhiteBlouseToCart();
            await _addToCartPage.ProceedToCheckoutFromCart();
            await _addToCartPage.SummaryCheckout();
            await _addToCartPage.AddressCheckout();
            await _addToCartPage.ShippingCheckout();

            // --- Database check
            try
            {
                var price = await DatabaseHelper.GetPriceAsync();

                Assert.IsNotNull(price, "Price should not be null");
                Assert.Greater(price.Value, 0, "The product price should be greater than 0");

                TestContext.WriteLine($"Retrieved Price: {price}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Database operation failed: {ex.Message}");
                Assert.Fail("Failed to retrieve price from DB");
            }

            await _addToCartPage.SelectPaymentMethod();
            await _addToCartPage.ConfirmPayment();
        }
    }
}