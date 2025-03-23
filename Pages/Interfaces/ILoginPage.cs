using Microsoft.Playwright;
using System.Threading.Tasks;

namespace E2ETestAutomation.Pages.Interfaces
{
    public interface ILoginPage
    {
        ILocator LoginAssertion { get; }
        Task GotoLoginPage();
        Task LoginAction(string username, string password);
    }
}