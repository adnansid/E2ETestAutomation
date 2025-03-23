using Microsoft.Playwright;

namespace E2ETestAutomation.Pages.Interfaces
{
    public interface ILoginPageLocator
    {
        ILocator Username { get; }
        ILocator Password { get; }
        ILocator SignIn { get; }
        ILocator LoginAssertion { get; }
        ILocator LoginButton { get; }
        ILocator ErrorMessage { get; }
        ILocator ShowPassword { get; }
    }
}