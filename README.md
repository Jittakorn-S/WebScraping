

# 🌟 WebScraping Project

A beautifully crafted C# project for scraping lottery results from the web and saving them to a CSV file. This project uses **Selenium WebDriver** to navigate and extract data from lottery websites, specifically targeting Lao lottery results. It’s designed to be robust, efficient, and easy to extend for additional scraping tasks.

---

## 📋 Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [How It Works](#how-it-works)
- [Contributing](#contributing)
- [License](#license)

---

## ✨ Features
- **Automated Web Scraping**: Extracts Lao lottery results (date, first prize, second prize) from a specified website.
- **Headless Browser**: Uses Selenium WebDriver in headless mode for efficient scraping without a visible browser window.
- **CSV Output**: Saves scraped data to a well-formatted CSV file using CsvHelper.
- **Error Handling**: Robust error handling for stable scraping across multiple pages.
- **Extensible Design**: Easily adaptable for scraping other websites or adding new features.

---

## 🛠 Technologies Used
- **C#**: Core programming language for the project.
- **.NET 9.0**: Modern .NET framework for performance and compatibility.
- **Selenium WebDriver**: For browser automation and web scraping.
- **CsvHelper**: For generating CSV files with scraped data.
- **HtmlAgilityPack**: Included for potential HTML parsing (not currently used in the main script).
- **Visual Studio 2022**: Recommended IDE for development and debugging.

---

## 🚀 Getting Started

### Prerequisites
- **.NET 9.0 SDK**: Ensure you have the .NET 9.0 SDK installed. [Download here](https://dotnet.microsoft.com/download/dotnet/9.0).
- **Visual Studio 2022**: Recommended for managing the solution and dependencies.
- **Chrome Browser**: Required for Selenium WebDriver (ChromeDriver).
- **NuGet Packages**: Dependencies are managed centrally via `Directory.Packages.props`.

### Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/WebScraping.git
   cd WebScraping
   ```

2. **Open the Solution**:
   - Open `WebScraping.sln` in Visual Studio 2022.

3. **Restore NuGet Packages**:
   - Visual Studio will automatically restore the required packages (`Selenium.WebDriver`, `CsvHelper`, etc.) based on `Directory.Packages.props`.

4. **Set Up ChromeDriver**:
   - The project uses `Selenium.WebDriver.ChromeDriver`, which automatically manages ChromeDriver installation.

5. **Build the Solution**:
   - Build the solution in Visual Studio to ensure all dependencies are resolved.

---

## 🎮 Usage
1. **Run the Application**:
   - Set `WebScraping` as the startup project.
   - Run in **Debug** or **Release** mode.
   - The program will:
     - Navigate to the specified lottery results page (`https://expalert.com/backward/laosdevelops`).
     - Scrape data (date, first prize, second prize) from each page.
     - Save the results to `lottery_results.csv` in the project directory.

2. **Output**:
   - Check the `lottery_results.csv` file for the scraped data.
   - The CSV file includes columns: `Date`, `FirstPrize`, `SecondPrize`.

3. **Logs**:
   - The console displays progress (e.g., "Navigating to next page..." or "No more pages.").
   - Errors, if any, are logged to the console for debugging.

---

## 📂 Project Structure
```
WebScraping/
├── WebScraping.sln                # Solution file
├── WebScraping/
│   ├── Program.cs                 # Main scraping logic
│   ├── WebScraping.csproj         # Project file with dependencies
│   ├── WebScraping.csproj.user    # User-specific settings
├── Directory.Packages.props       # Centralized package versions
├── WebScrapingThai.sln            # Separate solution for Thai lottery (not implemented)
└── README.md                      # This file
```

---

## 🔍 How It Works
1. **Browser Automation**:
   - The program initializes a headless Chrome browser using `Selenium.WebDriver`.
   - It navigates to the target URL (`https://expalert.com/backward/laosdevelops`).

2. **Data Extraction**:
   - Identifies grid rows (`.mantine-Grid-root`) containing lottery data.
   - Extracts `Date`, `FirstPrize`, and `SecondPrize` using CSS selectors.
   - Uses regex to clean and extract numeric values from prize fields.

3. **Pagination**:
   - Automatically clicks the "Next Page" link until no more pages are available.
   - Includes a delay (`Thread.Sleep(2500)`) to ensure pages load properly.

4. **Data Storage**:
   - Stores results in a `List<LuckyNumber>`.
   - Writes the data to `lottery_results.csv` using `CsvHelper`.

5. **Error Handling**:
   - Uses a custom `FindElementSafe` extension to avoid exceptions on missing elements.
   - Catches and logs errors during row parsing or navigation.

---

## 🤝 Contributing
Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit (`git commit -m "Add your feature"`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a Pull Request.

Please ensure your code follows the existing style and includes appropriate error handling.

---

## 📜 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

🌟 **Happy Scraping!** 🌟

