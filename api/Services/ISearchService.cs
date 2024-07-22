using api.DTOs;

namespace api.Services
{
    public interface ISearchService
    {
        public Task<ResultDTO<SearchDTO>> Search(string url, string search);

        public Task<List<SearchDTO>> GetSearchResult();
    }
}
