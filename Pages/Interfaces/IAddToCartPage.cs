using Microsoft.Playwright;
using System.Threading.Tasks;

namespace E2ETestAutomation.Pages.Interfaces
{
    public interface IAddToCartPage
    {
        Task NavigateToDressTab();
        Task SelectBlouse();
        Task AddWhiteBlouseToCart();
        Task ProceedToCheckoutFromCart();
        Task SummaryCheckout();
        Task AddressCheckout();
        Task ShippingCheckout();
        Task SelectPaymentMethod();
        Task ConfirmPayment();

        // Exposed locators for assertions
        ILocator DressPageAssert { get; }
        ILocator ListItem { get; }
        ILocator BtnAddToCart { get; }
        ILocator ProceedToCheckout { get; }
        ILocator CartTitle { get; }
        ILocator AssertAddressPage { get; }
        ILocator AsserShippingPage { get; }
        ILocator UnitPrice { get; }
        ILocator OrderedQuantity { get; }
        ILocator OrderedTotalPrice { get; }
        ILocator TotalPriceWithoutShipping { get; }
        ILocator ShippingCost { get; }
        ILocator TotalPriceWithShipping { get; }
        ILocator CheckPaymentPage { get; }
        ILocator OrderSuccessfull { get; }

        // Helper method
        Task<float> RemoveDollarAndConvert(string value);
    }
}