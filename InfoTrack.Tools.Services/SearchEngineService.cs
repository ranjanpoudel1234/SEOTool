using CSharpFunctionalExtensions;
using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Interfaces;
using InfoTrack.Tools.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTrack.Tools.Services
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly ISearchEngineRepository _searchEngineRepository;

        public SearchEngineService(ISearchEngineRepository searchEngineRepository)
        {
            _searchEngineRepository = searchEngineRepository;
        }

        public Task<Result<SearchResponse>> SearchAsync(SearchRequestParameter requestParameter, CancellationToken ct,
            SearchSourceTypes searchSouceType = SearchSourceTypes.Google)
        {
            return _searchEngineRepository.SearchAsync(requestParameter, ct, searchSouceType);
        }
    }
}
