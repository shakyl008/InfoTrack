using api.Data;
using api.DTOs;

namespace api.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHtmlUtils _htmlUtils;

        public SearchService(IHttpClientFactory httpClientFactory, ISearchRepository searchRepository, IHtmlUtils htmlUtils)
        {
            _httpClientFactory = httpClientFactory;
            _searchRepository = searchRepository;
            _htmlUtils = htmlUtils;
        }

        public async Task<ResultDTO<SearchDTO>> Search(string url, string search)
        {
            //create search string from url and keywoord
            var searchString = SearchEngineDTO.CreateSearchString(url, search);

            try
            {
                var response = await _httpClientFactory.CreateClient("default").GetAsync(searchString);

                if (!response.IsSuccessStatusCode)
                {
                    // check if response is valid before parsing response
                    return ResultDTO<SearchDTO>.Failure(null, error: response.ReasonPhrase);
                }

                //parse response
                var htmlString = await response.Content.ReadAsStringAsync();
                var urlEnum = SearchEngineDTO.GetSearchEnineEnum(url);
                var positions = _htmlUtils.ParseHtmlAndCountPositions(htmlString, urlEnum);
                if (!positions.IsSucess)
                {
                    return ResultDTO<SearchDTO>.Failure(null, error: positions.Error);
                }


                //create data model
                var searchDto = new SearchDTO
                {
                    SearchQuery = search,
                    Url = url,
                    Positions = positions.Value,
                    SearchDate = DateTime.Now,
                };

                // add to database
                await _searchRepository.AddSearchResult(searchDto);

                return ResultDTO<SearchDTO>.Success(searchDto);
            }
            catch (Exception ex)
            {
                // a lot can go wrong with the http client
                // usually i would set up a logger and a retry policy 
                // for now, just throwing the ex and returning the ex.message
                throw;
            }
        }


        public async Task<List<SearchDTO>> GetSearchResult()
        {
            return await _searchRepository.GetSearchResult();
        }

    }
}
