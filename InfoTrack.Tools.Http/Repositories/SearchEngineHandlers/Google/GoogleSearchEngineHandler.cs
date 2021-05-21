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

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google
{
    public class GoogleSearchEngineHandler : ISearchEngineHandler
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public GoogleSearchEngineHandler(SearchSettings searchSettings)
        {
            _apiKey = searchSettings.ApiKey;
            _baseUrl = searchSettings.BaseUrl;
        }

        public bool ShouldHandle(SearchSourceTypes searchSourceTypes)
        {
            return searchSourceTypes == SearchSourceTypes.Google;
        }

        public async Task<Result<SearchResponse>> HandleAsync(SearchRequestParameter searchRequestParameter, CancellationToken ct)
        {
            try
            {
                var endpoint = new SearchRequestDto(searchRequestParameter.Keywords,
                    _apiKey, new SearchPaginationRequest()).BuildRequestEndpoint(_baseUrl);

                var client = new HttpClient();

                var response = await client.GetAsync(endpoint, ct);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrWhiteSpace(responseBody))
                    return Result.Failure<SearchResponse>(ErrorMessages.ResponseBodyIsEmpty);

                var jObjectResult = JObject.Parse(responseBody);
                var searchResultJToken = jObjectResult[SearchResultDto.ResultListKey];
                if (searchResultJToken == null)
                    return Result.Failure<SearchResponse>(ErrorMessages.ResponseBodyIsEmpty);
                
                var searchResultDtoList = searchResultJToken.ToObject<IList<SearchResultDto>>();
                var searchResponse = new SearchResponse
                {
                    ResultPositions = searchResultDtoList
                        .Where(x => x.Url.Contains(searchRequestParameter.Url,
                            StringComparison.CurrentCultureIgnoreCase))
                        .OrderBy(x => x.Order)
                        .Select(x => x.Order)
                        .ToList().ToList()
                };

                return Result.Success(searchResponse);

            }
            catch (HttpRequestException e)
            {
                // log exception detail here using some logging helpers like Serilog
                return Result.Failure<SearchResponse>(ErrorMessages.FailureToLoadSearchResults);
            }
        }
    }
}
