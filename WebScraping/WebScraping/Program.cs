using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

class Program
{
    static void Main()
    {
        var options = new ChromeOptions();
        options.AddArgument("headless");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--no-sandbox");

        // Auto-download correct ChromeDriver version
        new DriverManager().SetUpDriver(new ChromeConfig());
        IWebDriver driver = new ChromeDriver(options);

        try
        {
            string baseUrl = "https://expalert.com/backward/laosdevelops";
            string currentPageUrl = baseUrl;

            var results = new List<LaoLotteryResult>();

            int pageCount = 1;
            const int maxPages = 10; // Prevent infinite loop if site changes

            while (pageCount <= maxPages)
            {
                Console.WriteLine($"Navigating to page {pageCount}...");
                driver.Navigate().GoToUrl(currentPageUrl);

                // Wait for content to load
                Thread.Sleep(2000);

                // Extract all rows
                var rows = driver.FindElements(By.CssSelector(".mantine-Grid-root .mantine-Grid-inner"));

                Console.WriteLine($"Found {rows.Count} result rows.");

                foreach (var row in rows)
                {
                    try
                    {
                        // Find columns inside each row
                        var cols = row.FindElements(By.CssSelector(".mantine-Grid-col"));
                        if (cols.Count >= 3)
                        {
                            string drawInfo = cols[0].Text.Trim(); // e.g., "ลาวพัฒนา | 5 พ.ค. 68"
                            string threeDigit = cols[1].Text.Trim(); // e.g., "279"
                            string twoDigit = cols[2].Text.Trim();   // e.g., "62"

                            // Skip invalid rows or rows with no numbers
                            if (threeDigit == "งดออกผล" || twoDigit == "งดออกผล")
                                continue;

                            // Extract only the date part after pipe symbol
                            string dateOnly = Regex.Match(drawInfo, @"\|\s*(.+)$")?.Groups[1]?.Value ?? "ไม่ระบุ";

                            results.Add(new LaoLotteryResult
                            {
                                DrawDate = WrapAsText(dateOnly),
                                ThreeDigitNumber = WrapAsText(threeDigit),
                                TwoDigitNumber = WrapAsText(twoDigit)
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error parsing row: " + ex.Message);
                    }
                }

                // Try to find next page link
                var nextPageLink = driver.FindElementSafe(By.XPath("//a[@title='ผลหวยลาวพัฒนาย้อนหลัง หน้าต่อไป']"));

                if (nextPageLink != null && !nextPageLink.GetAttribute("href").Contains("javascript"))
                {
                    currentPageUrl = nextPageLink.GetAttribute("href");
                    pageCount++;
                }
                else
                {
                    Console.WriteLine("No more pages.");
                    break;
                }
            }

            // Export to CSV
            string outputFilePath = "lao_lottery_results.csv";
            using (var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(results);
            }

            Console.WriteLine($"✅ Successfully saved {results.Count} records to '{outputFilePath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ An error occurred: " + ex.Message);
        }
        finally
        {
            driver.Quit();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }
    }

    // Helper: Wrap value as Excel text to avoid scientific notation
    static string WrapAsText(string value) => $"=\"{value}\"";
}

// Model class to store each row of data
public class LaoLotteryResult
{
    public string DrawDate { get; set; }
    public string ThreeDigitNumber { get; set; }
    public string TwoDigitNumber { get; set; }
}

// Extension method to safely find element
public static class WebDriverExtensions
{
    public static IWebElement FindElementSafe(this IWebDriver driver, By by)
    {
        try
        {
            return driver.FindElement(by);
        }
        catch
        {
            return null;
        }
    }
}