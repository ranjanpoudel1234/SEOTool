using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google
{

    public class SearchRequestDto
    {
        private const string SearchQueryTermKey = "q";
        private const string SearchQueryTermOqKey = "oq";
        private const string LanguageKey = "hl";
        private const string ApiContentTypeKey = "output";
        private const string ApiRequestSourceKey = "source";
        private const string KeyForApiKey = "api_key";
        private const string StartPageKey = "start";
        private const string LimitKey = "num";

        public string SearchQueryTerm { get; set; }
        public string SearchLanguageTerm { get; set; }
        public string ApiContentType { get; set; }
        public string ApiRequestSource { get; set; }
        public string ApiKey { get; set; }
        public SearchPaginationRequest PaginationRequest { get; set; }

        public SearchRequestDto(string searchQueryTerm, SearchPaginationRequest paginationRequest,
            string apiContentType = "json", string apiRequestSource = "dotnet", string apiKey = null, string language = "en")
        {
            SearchQueryTerm = searchQueryTerm;
            ApiContentType = apiContentType;
            ApiRequestSource = apiRequestSource;
            ApiKey = apiKey;
            PaginationRequest = paginationRequest;
            SearchLanguageTerm = language;
        }

        public string BuildRequestEndpoint(string baseUrl, bool useApiCall = false)
        {
            var keyValue = new Dictionary<string, string>();
            keyValue.Add(SearchQueryTermKey, SearchQueryTerm);
            keyValue.Add(SearchQueryTermOqKey, SearchQueryTerm);
            keyValue.Add(LanguageKey, SearchLanguageTerm);
            keyValue.Add(StartPageKey, PaginationRequest.StartPage);
            keyValue.Add(LimitKey, PaginationRequest.Limit);

            if (useApiCall)
            {
                keyValue.Add(ApiContentTypeKey, ApiContentType);
                keyValue.Add(ApiRequestSourceKey, ApiRequestSource);
                keyValue.Add(KeyForApiKey, ApiKey);
            }

            var fullUrl = QueryHelpers.AddQueryString(baseUrl, keyValue);
            return fullUrl;
        }
    }

    public class SearchPaginationRequest
    {
        public const int StartPageIndex = 0;
        public const int PageSize = 100;

        public SearchPaginationRequest(string startPage =null, string limit = null)
        {
            StartPage = startPage ?? StartPageIndex.ToString();
            Limit = limit ?? PageSize.ToString();
        }

        public string StartPage { get; set; }
        public string Limit { get; set; }
    }
}
