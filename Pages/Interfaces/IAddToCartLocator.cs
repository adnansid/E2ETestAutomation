using Microsoft.Playwright;

namespace E2ETestAutomation.Pages.Interfaces
{
    public interface IAddToCartLocator
    {
        ILocator DressTab { get; }
        ILocator DressPageAssert { get; }
        ILocator ListItemByText { get; }
        ILocator ListItem { get; }
        ILocator WhiteColor { get; }
        ILocator BtnAddToCart { get; }
        ILocator ProceedToCheckout { get; }
        ILocator BtnSummaryCheckout { get; }
        ILocator CartTitle { get; }
        ILocator AssertAddressPage { get; }
        ILocator BtnAddressCheckoutPage { get; }
        ILocator AsserShippingPage { get; }
        ILocator TermsAndConditionBox { get; }
        ILocator BtnShippingCheckoutPage { get; }
        ILocator UnitPrice { get; }
        ILocator OrderedQuantity { get; }
        ILocator OrderedTotalPrice { get; }
        ILocator TotalPriceWithoutShipping { get; }
        ILocator ShippingCost { get; }
        ILocator TotalPriceWithShipping { get; }
        ILocator RegisterForm { get; }
        ILocator VerifyCartElementQuantity { get; }
        ILocator BtnProceedToCheckoutOnAddressPage { get; }
        ILocator PaymentMethod { get; }
        ILocator CheckPaymentPage { get; }
        ILocator ConifrmOrder { get; }
        ILocator OrderSuccessfull { get; }
    }
}