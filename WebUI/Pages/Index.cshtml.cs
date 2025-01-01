using BusinessLayer;
using EntitiyLayer.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ArticleManager _articleManager;
        private readonly ILogger<IndexModel> _logger;

        public List<Article> Articles { get; set; } = new List<Article>();
        public string ErrorMessage { get; set; } = string.Empty;

        public IndexModel(ArticleManager articleManager, ILogger<IndexModel> logger)
        {
            _articleManager = articleManager;
            _logger = logger;
        }

        // GET: Sayfa ilk yüklendiğinde veya arama yapıldığında
        public void OnGet(string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    // Arama sorgusu mevcutsa, filtrelenmiş sonuçları getir
                    Articles = _articleManager.Search(query);
                }
                else
                {
                    // Aksi halde tüm verileri getir
                    Articles = _articleManager.GetAllArticles();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veri çekme sırasında hata oluştu.");
                ErrorMessage = "Veriler yüklenirken bir hata oluştu.";
            }
        }

        // POST: Veri çekme ve Elasticsearch'e kaydetme işlemi
        public void OnPost()
        {
            try
            {
                _articleManager.CrawlAndSave("https://www.sozcu.com.tr");
                Articles = _articleManager.GetAllArticles();
                ErrorMessage = "Veri başarıyla çekildi ve kaydedildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veri kaydetme sırasında hata oluştu.");
                ErrorMessage = "Veri kaydedilirken bir hata oluştu.";
            }
        }
    }
}