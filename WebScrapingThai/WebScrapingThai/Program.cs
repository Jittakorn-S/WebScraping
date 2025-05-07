using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string startUrl = "https://news.sanook.com/lotto/archive/";
        string outputPath = "sanook_lottery_results_all.csv";

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Background run
        using var driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl(startUrl);
        Thread.Sleep(3000);

        var results = new List<(string Date, string FirstPrize)>();

        while (true)
        {
            Thread.Sleep(2000); // Wait for page render

            var articles = driver.FindElements(By.CssSelector("article.archive--lotto"));
            foreach (var article in articles)
            {
                try
                {
                    // Get date
                    var timeElem = article.FindElement(By.CssSelector("time.archive--lotto__date"));
                    string drawDate = timeElem.GetAttribute("datetime")?.Trim();

                    var liElems = article.FindElements(By.CssSelector("ul.archive--lotto__result-list li"));
                    foreach (var li in liElems)
                    {
                        string label = li.FindElement(By.CssSelector("em.archive--lotto__result-txt")).Text.Trim();
                        if (label.Contains("รางวัลที่ 1"))
                        {
                            string prize = li.FindElement(By.CssSelector("strong.archive--lotto__result-number")).Text.Trim();
                            results.Add((drawDate, prize));
                            Console.WriteLine($"{drawDate}\t{prize}");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in article block: " + ex.Message);
                }
            }

            // Check for "next" button
            try
            {
                var nextButton = driver.FindElement(By.CssSelector("a.pagination__item--next"));
                string nextPageUrl = nextButton.GetAttribute("href");

                if (string.IsNullOrEmpty(nextPageUrl))
                    break;

                driver.Navigate().GoToUrl(nextPageUrl);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("✅ Reached last page.");
                break;
            }
        }

        driver.Quit();

        // Save to CSV
        using var writer = new StreamWriter(outputPath, false, Encoding.UTF8);
        writer.WriteLine("Draw Date,First Prize");

        foreach (var entry in results)
        {
            string dateCell = $"{entry.Date}";
            string prizeCell = $"{entry.FirstPrize}";
            writer.WriteLine($"{dateCell},{prizeCell}");
        }

        Console.WriteLine($"\n✅ All data saved to: {outputPath}");
    }
}