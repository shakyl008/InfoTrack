using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchDbContext _context;

        public SearchRepository(SearchDbContext context)
        {
            _context = context;
        }

        public async Task<List<SearchDTO>> GetSearchResult()
        {
            try
            {
                var results = await _context.Searches.ToListAsync();

                var resultDto = new List<SearchDTO>();

                foreach (var result in results)
                {
                    resultDto.Add(SearchDTO.CreateSearchDTO(result));
                }

                return resultDto;
            }
            catch (Exception ex) 
            {
                //log the exception 
                throw;
            }


        }

        public async Task<bool> AddSearchResult(SearchDTO searchDTO)
        {
            try
            {
                var searchEFM = searchDTO.CreateSeachEFModel();
                _context.Searches.Add(searchEFM);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
