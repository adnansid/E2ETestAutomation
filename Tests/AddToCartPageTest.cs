using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using static Microsoft.Playwright.Assertions;
using NUnit.Framework;
using E2ETestAutomation.Pages;
using E2ETestAutomation.Utilities;
using System.IO;
using System.Threading.Tasks;
using E2ETestAutomation.Pages.Interfaces;

namespace E2ETestAutomation.Tests
{
    [TestFixture]
    public class AddToCartPageTest : PageTest
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
        public async Task AddToCart()
        {
            // Login
            await _loginPage.GotoLoginPage();
            await _loginPage.LoginAction(_testData[1].Username, _testData[1].Password);
            Console.WriteLine($"Logging in with credentials: {_testData[1].Username} {_testData[1].Password}");
            await Expect(_loginPage.LoginAssertion).ToBeVisibleAsync();

            // Step 1: Navigate to Dress tab and assert
            await _addToCartPage.NavigateToDressTab();
            await Expect(_addToCartPage.DressPageAssert).ToBeVisibleAsync();

            // Step 2: Select blouse and assert
            await Expect(_addToCartPage.ListItem).ToBeVisibleAsync();
            await _addToCartPage.SelectBlouse();            
            

            // Step 3: Add to cart and assert
            await _addToCartPage.AddWhiteBlouseToCart();           
            await Expect(_addToCartPage.ProceedToCheckout).ToBeVisibleAsync();

            // Step 4: Proceed to checkout and assert
            await _addToCartPage.ProceedToCheckoutFromCart();
            await Expect(_addToCartPage.CartTitle).ToBeVisibleAsync();

            // Step 5: Summary checkout
            await _addToCartPage.SummaryCheckout();

            // Step 6: Address checkout and assert
            await Expect(_addToCartPage.AssertAddressPage).ToHaveTextAsync("Addresses");
            await _addToCartPage.AddressCheckout();

            // Step 7: Shipping checkout and assert
            await Expect(_addToCartPage.AsserShippingPage).ToHaveTextAsync("Shipping:");
            await _addToCartPage.ShippingCheckout();

            await Expect(_addToCartPage.UnitPrice).ToBeVisibleAsync();
            var unitPriceText = await _addToCartPage.UnitPrice.TextContentAsync();
            var unitPrice = await _addToCartPage.RemoveDollarAndConvert(unitPriceText ?? string.Empty);

            await Expect(_addToCartPage.OrderedQuantity).ToBeVisibleAsync();
            var orderedQuantityText = await _addToCartPage.OrderedQuantity.TextContentAsync();
            var orderedQuantity = float.Parse(orderedQuantityText ?? "0"); 

            var orderedTotalPriceText = await _addToCartPage.OrderedTotalPrice.TextContentAsync();
            var orderedTotalPrice = await _addToCartPage.RemoveDollarAndConvert(orderedTotalPriceText ?? string.Empty);

            var totalPriceWithoutShippingText = await _addToCartPage.TotalPriceWithoutShipping.TextContentAsync();
            var totalPriceWithoutShipping = await _addToCartPage.RemoveDollarAndConvert(totalPriceWithoutShippingText ?? string.Empty);

            var shippingCostText = await _addToCartPage.ShippingCost.TextContentAsync();
            var shippingCost = await _addToCartPage.RemoveDollarAndConvert(shippingCostText ?? string.Empty);

            var totalPriceWithShippingText = await _addToCartPage.TotalPriceWithShipping.TextContentAsync();
            var totalPriceWithShipping = await _addToCartPage.RemoveDollarAndConvert(totalPriceWithShippingText ?? string.Empty);

            var totalCost = (unitPrice * orderedQuantity) + shippingCost;
            Assert.AreEqual(34.0, totalCost);

            // Step 8: Payment and order confirmation
            await _addToCartPage.SelectPaymentMethod();
            await Expect(_addToCartPage.CheckPaymentPage).ToHaveTextAsync("Check payment");
            await _addToCartPage.ConfirmPayment();
            await Expect(_addToCartPage.OrderSuccessfull).ToHaveTextAsync("Your order on My Shop is complete.");

        }
    }
}