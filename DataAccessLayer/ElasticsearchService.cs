using EntitiyLayer.Entities;
using Nest;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("articles")
                .DisableDirectStreaming();  // Hata ayıklama için gereklidir
            _client = new ElasticClient(settings);
        }

        public void IndexArticles(List<Article> articles)
        {
            try
            {
                foreach (var article in articles)
                {
                    var indexRequest = new IndexRequest<Article>(article, "articles", "_doc", article.Id);
                    var indexResponse = _client.Index(indexRequest);
                    if (!indexResponse.IsValid)
                    {
                        Console.WriteLine($"Hata indeksleme işlemi sırasında: {indexResponse.DebugInformation}");
                    }
                    else
                    {
                        Console.WriteLine($"Article indexed: Title = {article.Title}, URL = {article.Url}, PublishDate = {article.PublishDate}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri indeksleme sırasında bir hata oluştu: {ex.Message}");
            }
        }


        public List<Article> SearchArticles(string query)
        {
            try
            {
                var searchResponse = _client.Search<Article>(s => s
                    .Query(q => q.MultiMatch(m => m
                        .Fields(f => f.Field(p => p.Title).Field(p => p.Url))
                        .Query(query)
                    )));

                Console.WriteLine($"Search Query: {query}");
                if (searchResponse.HitsMetadata != null)
                {
                    Console.WriteLine($"Total Hits: {searchResponse.HitsMetadata.Total}");
                    foreach (var hit in searchResponse.Hits)
                    {
                        Console.WriteLine($"Found Article: Title = {hit.Source.Title}, URL = {hit.Source.Url}");
                    }
                }
                else
                {
                    Console.WriteLine("Search Response Metadata is null.");
                }

                return searchResponse.Documents.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Arama sırasında hata oluştu: {ex.Message}");
                return new List<Article>();
            }
        }
    }
}