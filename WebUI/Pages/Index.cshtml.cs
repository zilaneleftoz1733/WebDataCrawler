using BusinessLayer;
using EntitiyLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ArticleManager _articleManager;
        private readonly ILogger<IndexModel> _logger;

        public List<Article> Articles { get; set; } = new List<Article>();
        public string ErrorMessage { get; set; } = string.Empty;

        // Constructor Dependency Injection
        public IndexModel(ArticleManager articleManager, ILogger<IndexModel> logger)
        {
            _articleManager = articleManager;
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                // Crawl ve Elasticsearch’e kaydet
                _articleManager.CrawlAndSave("https://www.sozcu.com.tr");

                // Elasticsearch’ten verileri çek
                Articles = _articleManager.GetAllArticles();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veri çekme veya kaydetme sırasında hata oluştu.");
                ErrorMessage = "Veriler yüklenirken bir hata oluştu.";
            }
        }
    }
}