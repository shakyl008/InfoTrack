using api.Models;
using System.ComponentModel;
using System.Reflection;

namespace api.DTOs
{
    public class SearchDTO
    {
        public int Id { get; set; }

        public string? SearchQuery { get; set; }

        public string? Url { get; set; }

        public string? Positions { get; set; }

        public DateTime? SearchDate { get; set; }


        public static SearchDTO CreateSearchDTO(Search searchModel)
        {
            var searchDto = new SearchDTO
            {
                Id = searchModel.Id,
                SearchQuery = searchModel.SearchQuery,
                Url = searchModel.Url,
                Positions = searchModel.Positions,
                SearchDate = searchModel.SearchDate,
            };

            return searchDto;
        }

        public Search CreateSeachEFModel()
        {
            var search = new Search 
            { 
                Id= this.Id,
                SearchQuery = this.SearchQuery,
                Url = this.Url,
                Positions = this.Positions,
                SearchDate = this.SearchDate,
            };

            return search;
        }
    }

}
