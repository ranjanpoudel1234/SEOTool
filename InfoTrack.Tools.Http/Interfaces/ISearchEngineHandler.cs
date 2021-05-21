using CSharpFunctionalExtensions;
using InfoTrack.Tools.Domain.Enums;
using InfoTrack.Tools.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTrack.Tools.Http.Interfaces
{
    public interface ISearchEngineHandler
    {
        bool ShouldHandle(SearchSourceTypes searchSourceTypes);
        Task<Result<SearchResponse>> HandleAsync(SearchRequestParameter searchRequestParameter, CancellationToken ct);
    }
}
