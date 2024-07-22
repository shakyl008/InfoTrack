using api.Data;
using api.DTOs;
using api.Services;
using Moq;
using Moq.Protected;
using System.Net;
using static api.DTOs.SearchEngineDTO;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Search_returns_dto()
        {
            var searchString = "land registry record";
            var url = "https://www.google.co.uk";

            // mock response message
            var mockHtml = File.ReadAllText("../../../test1.html");
            var httpClient = CreateMockHttpFactory(mockHtml, "SendAsync");

            // mock the data layer
            var mockSearchRepo = new Mock<ISearchRepository>();
            mockSearchRepo.Setup(repo => repo.AddSearchResult(It.IsAny<SearchDTO>())).ReturnsAsync(true);

            // act
            ISearchService searchService = new SearchService(httpClient, mockSearchRepo.Object, new HtmlUtils());
            var result = (await searchService.Search(url, searchString)).Value;

            // assert
            Assert.Equal("{0,39}", result.Positions);
            Assert.Equal("land registry record", result.SearchQuery);
            Assert.Equal("https://www.google.co.uk", result.Url);
        }

        [Theory]
        [InlineData("<div></div>", SearchEngines.Google, "No search data found")]
        [InlineData("<div></d", SearchEngines.Google, "No search data found")]
        [InlineData("", SearchEngines.Google, "No search data found")]
        public void ParseHtmlAndCountPositions_InvalidHtml_ReturnsFailure(string html, SearchEngines url, string expectedError)
        {
            // Arrange
            var htmlUtils = new HtmlUtils();

            // Act
            var result = htmlUtils.ParseHtmlAndCountPositions(html, url);

            // Assert
            Assert.False(result.IsSucess);
            Assert.Equal(expectedError, result.Error);
        }

        // a utility function i had written for previous projects
        public IHttpClientFactory CreateMockHttpFactory(string mockHtml, string httpMethod)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockHtml)
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    httpMethod,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            return httpClientFactoryMock.Object;
        }
    }
}