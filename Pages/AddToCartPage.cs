using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using E2ETestAutomation.Pages.Interfaces;

namespace E2ETestAutomation.Pages
{
    public class AddToCartPage : IAddToCartPage
    
    {
        private readonly IPage _page;
        private readonly IAddToCartLocator _selectors;

        public class LocatorCollection : IAddToCartLocator
        {
            public ILocator DressTab { get; }
            public ILocator DressPageAssert { get; }
            public ILocator ListItemByText { get; }
            public ILocator ListItem { get; }
            public ILocator WhiteColor { get; }
            public ILocator BtnAddToCart { get; }
            public ILocator ProceedToCheckout { get; }
            public ILocator BtnSummaryCheckout { get; }
            public ILocator CartTitle { get; }
            public ILocator AssertAddressPage { get; }
            public ILocator BtnAddressCheckoutPage { get; }
            public ILocator AsserShippingPage { get; }
            public ILocator TermsAndConditionBox { get; }
            public ILocator BtnShippingCheckoutPage { get; }
            public ILocator UnitPrice { get; }
            public ILocator OrderedQuantity { get; }
            public ILocator OrderedTotalPrice { get; }
            public ILocator TotalPriceWithoutShipping { get; }
            public ILocator ShippingCost { get; }
            public ILocator TotalPriceWithShipping { get; }
            public ILocator RegisterForm { get; }
            public ILocator VerifyCartElementQuantity { get; }
            public ILocator BtnProceedToCheckoutOnAddressPage { get; }
            public ILocator PaymentMethod { get; }
            public ILocator CheckPaymentPage { get; }
            public ILocator ConifrmOrder { get; }
            public ILocator OrderSuccessfull { get; }

            public LocatorCollection(IPage page)
            {
                DressTab = page.Locator("#block_top_menu > ul > li:nth-child(1) > a");
                DressPageAssert = page.Locator(".category-name:text('Women')");
                ListItemByText = page.Locator("text=Product available with different options").Nth(0);
                ListItem = page.Locator("h5[itemprop='name'] a[title='Blouse']");
                WhiteColor = page.Locator("#color_8");
                BtnAddToCart = page.Locator("#add_to_cart");
                ProceedToCheckout = page.Locator("#layer_cart a[title=\"Proceed to checkout\"]");
                BtnSummaryCheckout = page.Locator("#center_column a.button.btn.btn-default");
                CartTitle = page.Locator("#cart_title:text(\"Shopping-cart summary\")");
                AssertAddressPage = page.Locator("#center_column h1");
                BtnAddressCheckoutPage = page.Locator("[name='processAddress']");
                AsserShippingPage = page.Locator("#carrier_area > h1");
                TermsAndConditionBox = page.Locator("[name='cgv']");
                BtnShippingCheckoutPage = page.Locator("#form button");
                UnitPrice = page.Locator("li.price");
                OrderedQuantity = page.Locator("td.cart_quantity.text-center > span");
                OrderedTotalPrice = page.Locator("#total_price");
                TotalPriceWithoutShipping = page.Locator("#total_product");
                ShippingCost = page.Locator("#total_shipping");
                TotalPriceWithShipping = page.Locator("#total_price");
                RegisterForm = page.Locator("#add_address");
                VerifyCartElementQuantity = page.Locator("#summary_products_quantity");
                BtnProceedToCheckoutOnAddressPage = page.Locator("#center_column button");
                PaymentMethod = page.Locator(".cheque");
                CheckPaymentPage = page.Locator("#center_column > form > div > h3");
                ConifrmOrder = page.Locator("#cart_navigation > button");
                OrderSuccessfull = page.Locator("#center_column > p.alert.alert-success");
            }

        }

        public AddToCartPage(IPage page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
            _selectors = new LocatorCollection(page);
        }

        // Step-by-step methods for the Add to Cart process
        public async Task NavigateToDressTab()
        {
            await _selectors.DressTab.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task SelectBlouse()
        {
            await _selectors.ListItem.ScrollIntoViewIfNeededAsync();
            var elementHandle = await _selectors.ListItem.ElementHandleAsync();
            await _page.EvaluateAsync("element => element.click()", elementHandle);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            

        }

        public async Task AddWhiteBlouseToCart()
        {
            await _selectors.WhiteColor.ClickAsync();
            await _page.ReloadAsync();
            await _selectors.BtnAddToCart.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task ProceedToCheckoutFromCart()
        {
            await _selectors.ProceedToCheckout.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task SummaryCheckout()
        {
            await _selectors.BtnSummaryCheckout.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task AddressCheckout()
        {
            await _page.ReloadAsync();
            await _selectors.BtnAddressCheckoutPage.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task ShippingCheckout()
        {
            await _selectors.TermsAndConditionBox.CheckAsync();
            await _selectors.BtnShippingCheckoutPage.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task SelectPaymentMethod(){
            await _selectors.PaymentMethod.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task ConfirmPayment()
        {
            await _selectors.ConifrmOrder.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        // Expose locators for assertions
        public ILocator DressPageAssert => _selectors.DressPageAssert;
        public ILocator ListItem => _selectors.ListItem;
        public ILocator BtnAddToCart => _selectors.BtnAddToCart;
        public ILocator ProceedToCheckout => _selectors.ProceedToCheckout;
        public ILocator CartTitle => _selectors.CartTitle;
        public ILocator AssertAddressPage => _selectors.AssertAddressPage;
        public ILocator AsserShippingPage => _selectors.AsserShippingPage;
        public ILocator UnitPrice => _selectors.UnitPrice;
        public ILocator OrderedQuantity => _selectors.OrderedQuantity;
        public ILocator OrderedTotalPrice => _selectors.OrderedTotalPrice;
        public ILocator TotalPriceWithoutShipping => _selectors.TotalPriceWithoutShipping;
        public ILocator ShippingCost => _selectors.ShippingCost;
        public ILocator TotalPriceWithShipping => _selectors.TotalPriceWithShipping;
        public ILocator CheckPaymentPage => _selectors.CheckPaymentPage;
        public ILocator OrderSuccessfull => _selectors.OrderSuccessfull;

        // Helper method to extract and convert price values
        public async Task<float> RemoveDollarAndConvert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            var numericValue = float.TryParse(value.Replace("$", ""), out var result) ? result : 0;
            return await Task.FromResult(numericValue);
        }
    }
}