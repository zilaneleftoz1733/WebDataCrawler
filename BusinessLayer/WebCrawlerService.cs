using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using EntitiyLayer.Entities;

namespace DataAccessLayer
{
    public class WebCrawlerService
    {
        public List<Article> CrawlArticles(string url)
        {
            var articles = new List<Article>();

            try
            {
                var web = new HtmlWeb();
                // User-agent bilgisini Google Chrome olarak ayarlayalım
                web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";

                var doc = web.Load(url);

                // Tüm bağlantıları seçmek için @href özelliğine sahip tüm <a> etiketlerini çekelim
                var nodes = doc.DocumentNode.SelectNodes("//a[@href]");

                Console.WriteLine($"Found {nodes?.Count ?? 0} nodes.");

                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        var title = node.InnerText.Trim();
                        var urlAttribute = node.GetAttributeValue("href", string.Empty);

                        if (!string.IsNullOrEmpty(urlAttribute) && !string.IsNullOrEmpty(title))
                        {
                            // URL'leri doğru formatta alalım
                            var formattedUrl = urlAttribute.StartsWith("http") ? urlAttribute : "https://www.sozcu.com.tr" + urlAttribute;
                            var article = new Article
                            {
                                Id = Guid.NewGuid().ToString(),
                                Title = title,
                                Url = formattedUrl,
                                PublishDate = DateTime.Now
                            };
                            Console.WriteLine($"Article found: Title = {title}, URL = {formattedUrl}");
                            articles.Add(article);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No articles found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            return articles;
        }
    }
}
