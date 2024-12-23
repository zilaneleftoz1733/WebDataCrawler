using EntitiyLayer.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class WebCrawlerService
    {
        public List<Article> CrawlArticles(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var articles = new List<Article>();

            var nodes = doc.DocumentNode.SelectNodes("//a[contains(@href, '/2024/')]");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var titleNode = node.SelectSingleNode(".//h2") ?? node.SelectSingleNode(".//h3");
                    var urlAttribute = node.GetAttributeValue("href", string.Empty);

                    if (!string.IsNullOrEmpty(urlAttribute) && titleNode != null)
                    {
                        articles.Add(new Article
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = titleNode.InnerText.Trim(),
                            Url = urlAttribute.StartsWith("http") ? urlAttribute : "https://www.sozcu.com.tr" + urlAttribute,
                            PublishDate = DateTime.Now
                        });
                    }
                }
            }

            return articles;
        }
    }
}