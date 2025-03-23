using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using E2ETestAutomation.Pages.Interfaces;
using E2ETestAutomation.Utilities; 


namespace E2ETestAutomation.Pages
{
   public class LoginPage : ILoginPage
   {
       private readonly IPage _page;
       private readonly ILoginPageLocator _selectors;


       public class LocatorCollection : ILoginPageLocator
       {
           public ILocator Username { get; } = null!;
           public ILocator Password { get; } = null!;
           public ILocator SignIn { get; } = null!;
           public ILocator LoginAssertion { get; } = null!;
           public ILocator LoginButton { get; } = null!;
           public ILocator ErrorMessage { get; } = null!;
           public ILocator ShowPassword { get; } = null!;


           public LocatorCollection(IPage page)
           {
               Username = page.Locator("#email");
               Password = page.Locator("#passwd");
               SignIn = page.Locator("[title=\"Log in to your customer account\"]");
               LoginAssertion = page.Locator(".page-heading").GetByText("My account");
               LoginButton = page.Locator("#SubmitLogin");
               ErrorMessage = page.Locator("text=Authentication failed.");
               ShowPassword = page.Locator("label:has-text(\"Show password\")");
           }
       }


       public LoginPage(IPage page)
       {
           _page = page ?? throw new ArgumentNullException(nameof(page));
           _selectors = new LocatorCollection(page);
       }


       public ILocator LoginAssertion => _selectors.LoginAssertion;


       public async Task GotoLoginPage()
       {
           var baseUrl = ConfigReader.GetBaseUrl();
           await _page.GotoAsync($"{baseUrl}/index.php");
           await _selectors.SignIn.ClickAsync();
           await _selectors.Username.WaitForAsync();
       }


       public async Task LoginAction(string username, string password)
       {
           ValidateCredentials(username, password);


           await _selectors.Username.FillAsync(username);
           await _selectors.Password.FillAsync(password);
           await _selectors.LoginButton.ClickAsync();
       }


       private void ValidateCredentials(string username, string password)
       {
           if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
           {
               throw new ArgumentException("Username and password must be non-empty strings");
           }
       }
   }
}

