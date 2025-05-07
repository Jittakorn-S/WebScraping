using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var options = new ChromeOptions();
        options.AddArgument("headless"); // Run in headless mode
        IWebDriver driver = new ChromeDriver(options);

        try
        {
            driver.Navigate().GoToUrl("https://expalert.com/backward/laosdevelops");

            var results = new List<LuckyNumber>();

            while (true)
            {
                // Find all grid rows that contain lottery data
                var rows = driver.FindElements(By.CssSelector(".mantine-Grid-root"));

                foreach (var row in rows)
                {
                    try
                    {
                        // Only process rows with exactly 3 columns: Date, First Prize, Second Prize
                        var cols = row.FindElements(By.CssSelector(".mantine-Grid-col"));
                        if (cols.Count >= 3)
                        {
                            string dateText = cols[0].Text.Trim();
                            string firstPrizeRaw = cols[1].Text.Trim();
                            string secondPrizeRaw = cols[2].Text.Trim();

                            // Skip rows where no result was published
                            if (firstPrizeRaw == "งดออกผล" || secondPrizeRaw == "งดออกผล")
                                continue;

                            // Extract only numbers
                            string firstPrize = Regex.Match(firstPrizeRaw, @"\d{3}").Value;
                            string secondPrize = Regex.Match(secondPrizeRaw, @"\d{2}").Value;

                            results.Add(new LuckyNumber
                            {
                                Date = WrapAsText(dateText),
                                FirstPrize = WrapAsText(firstPrize),
                                SecondPrize = WrapAsText(secondPrize)
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error parsing row: " + ex.Message);
                    }
                }

                // Try to click "Next Page"
                var nextPageLink = driver.FindElementSafe(By.CssSelector("a[title='ผลหวยลาวพัฒนาย้อนหลัง หน้าต่อไป']"));

                if (nextPageLink != null && !nextPageLink.GetAttribute("href").Contains("javascript"))
                {
                    Console.WriteLine("Navigating to next page...");
                    nextPageLink.Click();
                    Thread.Sleep(2500); // Wait for page to load
                }
                else
                {
                    Console.WriteLine("No more pages.");
                    break;
                }
            }

            // Save to CSV
            using (var writer = new StreamWriter("lottery_results.csv", false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(results);
            }

            Console.WriteLine("✅ Data has been saved to 'lottery_results.csv'");
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

    // Helper method to wrap values as text in Excel
    static string WrapAsText(string value)
    {
        return $"=\"{value}\"";
    }
}

// Class to store the lottery data
public class LuckyNumber
{
    public string Date { get; set; }
    public string FirstPrize { get; set; }
    public string SecondPrize { get; set; }
}

// Extension method to safely find element without throwing exception
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