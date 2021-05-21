using CSharpFunctionalExtensions;
using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTrack.Tools.Domain.Interfaces
{
    public interface ISearchEngineService
    {
        public Task<Result<SearchResponse>> SearchAsync(SearchRequestParameter requestParameter,
            CancellationToken ct, SearchSourceTypes searchSouceType = SearchSourceTypes.Google);
    }
}
