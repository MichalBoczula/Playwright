using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace Playwright
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {
        [Test]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
        {
            await Page.GotoAsync("http://localhost:3000/");

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

            // create a locator
            var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

            // Expect an attribute "to be strictly equal" to the value.
            await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

            // Click the get started link.
            await getStarted.ClickAsync();

            // Expects the URL to contain intro.
            await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
        }

        [Test]
        public async Task Test()
        {
            await using var browser = await Playwright.Chromium.LaunchAsync(new()
            {
                Headless = false,
            });

            var page = await browser.NewPageAsync();
            await page.GotoAsync("http://localhost:3000/");

            await page.WaitForTimeoutAsync(2000);

            var element = await page.QuerySelectorAsync(".total-amount");
            var text = await element.TextContentAsync();

            await browser.CloseAsync();
        }
    }
}