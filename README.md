# Web Data Crawler and Elasticsearch Integration

This project is a web data crawler application that fetches article data from a specified website, indexes the data in Elasticsearch, and displays the articles through an ASP.NET Razor Pages web interface. The primary goal is to demonstrate the use of web crawling, Elasticsearch integration, and Razor Pages for data visualization.

## Features

- **Web Crawling**: Fetches article data from a specified URL.
- **Elasticsearch Integration**: Indexes and searches the fetched data in Elasticsearch.
- **ASP.NET Razor Pages**: Displays the article data in a web interface.
- **Search Functionality**: Provides a search bar to query the indexed articles.

## Technologies Used

- **.NET 6**
- **ASP.NET Razor Pages**
- **Elasticsearch**
- **NEST (Elasticsearch .NET Client)**
- **HtmlAgilityPack**

## Getting Started

### Prerequisites

- **.NET 6 SDK**: [Download and install .NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- **Elasticsearch**: [Download and install Elasticsearch](https://www.elastic.co/downloads/elasticsearch)
- **Elasticsearch NEST Client**: Add the NEST package to your project
  ```sh
  dotnet add package NEST --version 6.x
