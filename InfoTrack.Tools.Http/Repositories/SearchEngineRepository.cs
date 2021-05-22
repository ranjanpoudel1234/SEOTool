using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Models;
using InfoTrack.Tools.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using InfoTrack.Tools.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace InfoTrack.Tools.Http.Repositories
{
    public class SearchEngineRepository : ISearchEngineRepository
    {
        private readonly IEnumerable<ISearchEngineHandler> _handlers;
    
        public SearchEngineRepository(IEnumerable<ISearchEngineHandler> handlers)
        {
            _handlers = handlers;

        }

        public Task<Result<SearchResponse>> SearchAsync(SearchRequestParameter requestParameter, CancellationToken ct,  SearchSourceTypes searchSouceType = SearchSourceTypes.Google)
        {
           return GetHandlerToUse(searchSouceType).HandleAsync(requestParameter, ct);
        }

        private ISearchEngineHandler GetHandlerToUse(SearchSourceTypes searchSouceType)
        {
            var handler = _handlers.SingleOrDefault(x => x.ShouldHandle(searchSouceType));

            if (handler == null)
                throw new NotImplementedException($"No search handlers exist for {searchSouceType}");

            return handler;
        }
    }
}
