using api.DTOs;

namespace api.Data
{
    public interface ISearchRepository
    {
        public Task<List<SearchDTO>> GetSearchResult();

        public Task<bool> AddSearchResult(SearchDTO searchDTO);
    }
}
