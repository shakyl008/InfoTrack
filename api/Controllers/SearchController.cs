using api.DTOs;
using api.Models;
using api.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class SearchController : ControllerBase
    {
        private readonly SearchDbContext _context;
        private readonly ISearchService _searchService;

        public SearchController(SearchDbContext context, ISearchService searchService)
        {
            _context = context;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string url, string search )
        {
            if(string.IsNullOrEmpty(url) || string.IsNullOrEmpty(search))
                {
                throw new ArgumentNullException();
            }
            try
            {
                var result = await _searchService.Search(url, search);
                if (result.IsSucess)
                {
                    return Ok(result);
                }
                else if(result.Error == "No search data found")
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("getsearchresults")]
        public async Task<IActionResult> GetSearchResult()
        {
            return Ok(await _searchService.GetSearchResult());
        }
    }
}
