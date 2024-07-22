using System.Web;

namespace api.DTOs
{

    public static class SearchEngineDTO
    {
        public enum SearchEngines
        {
            Google,
            DuckDuckGo,
            Bing
        }

        public static string GetSearchEngineUrls(SearchEngines enumInput)
        {
            switch (enumInput)
            {
                case SearchEngines.Google:
                    return "https://www.google.co.uk";
                case SearchEngines.DuckDuckGo:
                    return "https://duckduckgo.com";
                case SearchEngines.Bing:
                    return "https://www.bing.com/";
                default:
                    throw new ArgumentOutOfRangeException("Only google, duckduckgo and bing implmented");
            }
        }

        public static SearchEngines GetSearchEnineEnum(string url)
        {
            switch (url)
            {
                case string s when s.Contains("google.", StringComparison.InvariantCultureIgnoreCase):
                    return SearchEngines.Google;
                case string s when s.Contains("duckduckgo.", StringComparison.InvariantCultureIgnoreCase):
                    return SearchEngines.DuckDuckGo;
                case string s when s.Contains("bing.", StringComparison.InvariantCultureIgnoreCase):
                    return SearchEngines.Bing;
                default:
                    throw new ArgumentException("Only google, duckduckgo and bing implmented");
            }
        }

        public static string CreateSearchString(string url, string keyword)
        {
            //encode search text 
            var encodedString = HttpUtility.UrlEncode(keyword);

            var engineType = GetSearchEnineEnum(url);

            switch (engineType)
            {
                case SearchEngines.Google:
                    return $"{url}/search?num=100&q={encodedString}";
                case SearchEngines.DuckDuckGo:
                    // could not find a way to specify number of results in duckduckgo
                    return $"{url}/?t=h_&q={encodedString}&ia=web";
                case SearchEngines.Bing:
                    return $"{url}/search?q={encodedString}";
                default:
                    throw new ArgumentException("Invalid input provided. Currently only google, duckduckgo and bing supported");
            }
        }
    }
}
