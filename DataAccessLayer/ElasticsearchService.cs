using EntitiyLayer.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("articles");
            _client = new ElasticClient(settings);
        }

        public void IndexArticles(List<Article> articles)
        {
            foreach (var article in articles)
            {
                var response = _client.IndexDocument(article);
                if (!response.IsValid)
                {
                    Console.WriteLine($"Error indexing article: {response.OriginalException.Message}");
                }
            }
        }

        public List<Article> SearchArticles(string query)
        {
            var searchResponse = _client.Search<Article>(s => s
                .Query(q => q.QueryString(d => d.Query($"*{query}*"))));

            return searchResponse.Documents.ToList();
        }
    }
}
