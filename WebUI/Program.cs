using BusinessLayer;
using DataAccessLayer;
using EntitiyLayer;

namespace WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)

        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:9200");
            //    var response = await client.GetAsync("/articles/_search");
            //    var result = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(result);
            //}

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ArticleManager>();
            builder.Services.AddScoped<ElasticsearchService>();
            builder.Services.AddScoped<WebCrawlerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}