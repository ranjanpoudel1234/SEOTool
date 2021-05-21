using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Models;
using InfoTrack.Tools.Http.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Bing
{
    public class BingSearchEngineHandler : ISearchEngineHandler
    {
        public bool ShouldHandle(SearchSourceTypes searchSourceTypes)
        {
            return searchSourceTypes == SearchSourceTypes.Bing;
        }

        public List<SearchResponse> Handle(SearchRequestParameter searchRequestParameter)
        {
            // this class can be extended later when/if we want to support Bing search
            throw new System.NotImplementedException();
        }

        public Task<Result<SearchResponse>> HandleAsync(SearchRequestParameter searchRequestParameter, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<SearchResponse>> HandleAsync(SearchRequestParameter searchRequestParameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
