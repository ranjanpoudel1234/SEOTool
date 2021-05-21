using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Models;

namespace InfoTrack.Tools.Domain.Interfaces
{
    public interface ISearchEngineRepository
    {
        public Task<Result<SearchResponse>> SearchAsync(SearchRequestParameter requestParameter,
            CancellationToken ct,  SearchSourceTypes searchSouceType = SearchSourceTypes.Google);
    }
}
