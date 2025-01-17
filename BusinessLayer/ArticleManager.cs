﻿using DataAccessLayer;
using EntitiyLayer.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class ArticleManager
    {
        private readonly WebCrawlerService _crawler;
        private readonly ElasticsearchService _elasticsearch;

        public ArticleManager()
        {
            _crawler = new WebCrawlerService();
            _elasticsearch = new ElasticsearchService();
        }

        public void CrawlAndSave(string url)
        {
            var articles = _crawler.CrawlArticles(url);
            _elasticsearch.IndexArticles(articles);
        }

        public List<Article> GetAllArticles()
        {
            try
            {
                return _elasticsearch.SearchArticles(string.Empty);  // Tüm makaleleri almak için boş bir arama sorgusu kullanabilirsiniz.
            }
            catch (Exception ex)
            {
                throw new Exception("Makale verileri alınırken hata oluştu.", ex);
            }
        }

        public List<Article> Search(string query)
        {
            return _elasticsearch.SearchArticles(query);
        }
    }
}