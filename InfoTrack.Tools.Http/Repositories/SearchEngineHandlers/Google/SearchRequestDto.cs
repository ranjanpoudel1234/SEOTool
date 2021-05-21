using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google
{

    public class SearchRequestDto
    {
        private const string SearchQueryTermKey = "q";
        private const string ApiContentTypeKey = "output";
        private const string ApiRequestSourceKey = "source";
        private const string KeyForApiKey = "api_key";
        private const string StartPageKey = "start";
        private const string LimitKey = "num";

        public string SearchQueryTerm { get; set; }
        public string ApiContentType { get; set; }
        public string ApiRequestSource { get; set; }
        public string ApiKey { get; set; }
        public SearchPaginationRequest PaginationRequest { get; set; }

        public SearchRequestDto(string searchQueryTerm, string apiKey, SearchPaginationRequest paginationRequest,
            string apiContentType = "json", string apiRequestSource = "dotnet")
        {
            SearchQueryTerm = searchQueryTerm;
            ApiContentType = apiContentType;
            ApiRequestSource = apiRequestSource;
            ApiKey = apiKey;
            PaginationRequest = paginationRequest;
        }

        public string BuildRequestEndpoint(string baseUrl)
        {
            var keyValue = new Dictionary<string, string>();
            keyValue.Add(SearchQueryTermKey, SearchQueryTerm);
            keyValue.Add(StartPageKey, PaginationRequest.StartPage);
            keyValue.Add(LimitKey, PaginationRequest.Limit);
            keyValue.Add(ApiContentTypeKey, ApiContentType);
            keyValue.Add(ApiRequestSourceKey, ApiRequestSource);
            keyValue.Add(KeyForApiKey, ApiKey);

            var fullUrl = QueryHelpers.AddQueryString(baseUrl, keyValue);
            return fullUrl;
        }
    }

    public class SearchPaginationRequest
    {
        public SearchPaginationRequest(string startPage = "0", string limit = "100")
        {
            StartPage = startPage;
            Limit = limit;
        }

        public string StartPage { get; set; }
        public string Limit { get; set; }
    }
}
