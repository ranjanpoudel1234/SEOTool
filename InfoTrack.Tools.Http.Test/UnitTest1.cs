using FakeItEasy;
using InfoTrack.Tools.Domain.Models;
using InfoTrack.Tools.Http.Interfaces;
using InfoTrack.Tools.Http.Repositories;
using InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InfoTrack.Tools.Domain.Interfaces;

namespace InfoTrack.Tools.Http.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task SearchAsync_WithParameterProvided_ReturnsResultAsync()
        {
            var searchEngineRepository = GetDefaultSearchEngineRepository();

            var result = await searchEngineRepository.SearchAsync(GetDefaultRequestParameter, CancellationToken.None);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
        }

        private SearchRequestParameter GetDefaultRequestParameter =>
            new SearchRequestParameter("efiling integration", "www.infotrack.com");

        private ISearchEngineRepository GetDefaultSearchEngineRepository(List<ISearchEngineHandler> searchEngineHandlers = null)
        {
            var googleSearchEngineHandlder = new GoogleSearchEngineHandler(A<SearchSettings>._);
            var handlerList = searchEngineHandlers ?? new List<ISearchEngineHandler> { googleSearchEngineHandlder };

            return new SearchEngineRepository(handlerList);
        }
    }
}
