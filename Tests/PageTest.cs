using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace E2ETestAutomation.Tests
{
    public abstract class PageTest : Microsoft.Playwright.NUnit.PageTest
    {
        protected IPlaywright Playwright { get; private set; }
        protected IBrowser Browser { get; private set; }
        protected IPage Page { get; private set; }
        
        protected string BaseUrl { get; private set; }

        [SetUp]
        public async Task BaseSetupAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var headless = ConfigReader.GetBrowserHeadless();
            var args = ConfigReader.GetBrowserArgs();

            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Args = args
            });

            var context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null
            });

            Page = await context.NewPageAsync();

            BaseUrl = ConfigReader.GetBaseUrl();
        }

        [TearDown]
        public async Task BaseTeardownAsync()
        {
            if (Browser != null)
            {
                await Browser.CloseAsync();
            }

            Playwright?.Dispose();
        }
        protected async Task GotoAsync(string relativePath)
        {
            var url = relativePath.StartsWith("/")
                ? $"{BaseUrl}{relativePath}"
                : $"{BaseUrl}/{relativePath}";
            await Page.GotoAsync(url);
        }
    }
}