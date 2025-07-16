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
