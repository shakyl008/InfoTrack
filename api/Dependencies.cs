using api.Data;
using api.Models;
using api.Services;

namespace api
{
    public class Dependencies
    {
        public static void AddDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<IHtmlUtils, HtmlUtils>();

            // tried to get around googles hourly limit - no sucess
            builder.Services.AddHttpClient("default", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
            });

            builder.Services.AddDbContext<SearchDbContext>();
            builder.Services.AddScoped<ISearchRepository, SearchRepository>();

        }
    }
}
