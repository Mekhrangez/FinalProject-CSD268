using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

namespace PlaywrightTests
{
    public class UserInterfaceTests
    {
        [Fact]
        public static async Task VerifyLoginToLetsUseData()
        {
            // Create Playwright instance
            using IPlaywright playwright = await Playwright.CreateAsync();

            // Launch browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50 });
            IBrowserContext context = await browser.NewContextAsync();

            // Create a new page
            IPage page = await context.NewPageAsync();

            // Navigate to letsusedata login page
            await page.GotoAsync("https://letsusedata.com");

            // Fill and submit login form for Test1
            await page.FillAsync("input[name='username']", "Test1");
            await page.FillAsync("input[name='password']", "12345678");
            await Task.WhenAll(
                page.WaitForNavigationAsync(),
                page.ClickAsync("button[type='submit']")
            );

            // Verify Test1 user's login
            Assert.Equal("https://letsusedata.com/CourseSelection.html", page.Url);

            // Logout Test1 user
            await page.ClickAsync("button=Logout");

            // Navigate to letsusedata login page again
            await page.GotoAsync("https://letsusedata.com");

            // Fill and submit login form for Test2
            await page.FillAsync("input[name='username']", "Test2");
            await page.FillAsync("input[name='password']", "iF3sBF7c");
            await Task.WhenAll(
                page.WaitForNavigationAsync(),
                page.ClickAsync("button[type='submit']")
            );

            // Verify Test2 user's login
            Assert.Equal("https://letsusedata.com/CourseSelection.html", page.Url);
        }
    }
}