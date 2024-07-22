using api.DTOs;

namespace api.Services
{
    public interface IHtmlUtils
    {
        public ResultDTO<string> ParseHtmlAndCountPositions(string html, SearchEngineDTO.SearchEngines url);
    }
}
