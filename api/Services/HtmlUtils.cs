using api.DTOs;
using HtmlAgilityPack;
using static api.DTOs.SearchEngineDTO;

namespace api.Services
{
    public class HtmlUtils : IHtmlUtils
    {
        private static HtmlNodeCollection FindSearchHits(SearchEngines url, HtmlDocument htmlDoc)
        {
            switch (url)
            {
                case SearchEngines.Google:
                    // divs containinig google search results
                    var colletion = new HtmlNodeCollection(htmlDoc.DocumentNode);
                    var sponsored = htmlDoc.DocumentNode.SelectNodes("/html/body/div/div/div/div/div/div/div/div");
                    if(sponsored != null)
                    {
                        foreach (var item in sponsored)
                        {
                            colletion.Add(item);
                        }
                    }

                    var organic = htmlDoc.DocumentNode.SelectNodes("/html/body/div/div/div/div/div/div/div/div/div/div");
                    if(organic != null)
                    {
                        foreach (var item in organic)
                        {
                            colletion.Add(item);
                        }
                    }

                    return colletion;

                case SearchEngines.DuckDuckGo:
                    // duckduckgo appears to have a nicer naming convention 
                    return htmlDoc.DocumentNode.SelectNodes("//li[contains(@data-layout, 'organic')]");
                case SearchEngines.Bing:
                    return htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'b_algo')]");
                default:
                    throw new Exception("No valid search engine");
            }
        }

        public ResultDTO<string> ParseHtmlAndCountPositions(string html, SearchEngines url)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);


            var searchDivs = FindSearchHits(url, htmlDoc);

            if (searchDivs == null || searchDivs.Count == 0)
            {
                return ResultDTO<string>.Failure(null, error: "No search data found");
            }

            string position = "{";
            for (int i = 0; i < searchDivs.Count; i++)
            {
                var flatDiv = searchDivs[i].WriteContentTo();
                if (flatDiv.Contains("InfoTrack", StringComparison.OrdinalIgnoreCase)) // made this case insensitive as sometimes infotrack was coming up all caps
                {
                    position += $",{i}";
                }
            }

            //clean up output
            if (position.Length > 1)
            {
                position = position.Remove(1, 1);
            }
            position += "}";

            return ResultDTO<string>.Success(position);
        }
    }
}
