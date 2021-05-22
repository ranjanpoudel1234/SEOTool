using CSharpFunctionalExtensions;
using InfoTrack.Tools.Domain;
using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Models;
using InfoTrack.Tools.Http.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google
{
    public class GoogleSearchEngineHandler : ISearchEngineHandler
    {
        private readonly string _baseUrl;
        private readonly ILogger _logger;

        public GoogleSearchEngineHandler(SearchSettings searchSettings, ILoggerFactory loggerFactory)
        {
            _baseUrl = searchSettings.BaseUrl;
            _logger = loggerFactory.CreateLogger(nameof(SearchEngineRepository));
        }

        public bool ShouldHandle(SearchSourceTypes searchSourceTypes)
        {
            return searchSourceTypes == SearchSourceTypes.Google;
        }

        public async Task<Result<SearchResponse>> HandleAsync(SearchRequestParameter searchRequestParameter, CancellationToken ct)
        {
            try
            {
                var endpoint = new SearchRequestDto(searchRequestParameter.Keywords, new SearchPaginationRequest())
                    .BuildRequestEndpoint(_baseUrl);

                var client = new HttpClient();

                var response = new HttpResponseMessage();
                await SimpleRetryHelper.RetryOnExceptionAsync(async () =>
                {
                    response = await client.GetAsync(endpoint, ct);

                }, _logger);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseBody))
                    return Result.Failure<SearchResponse>(ErrorMessages.ResponseBodyIsEmpty);

                var searchResponse = GoogleSearchResultParser.GetSearchResponseFromHtml(responseBody, searchRequestParameter.Url);

                return Result.Success(searchResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught while handling GoogleSearchEngineHandler", ex);
                return Result.Failure<SearchResponse>(ErrorMessages.FailureToLoadSearchResults);
            }
        }
    }
}
