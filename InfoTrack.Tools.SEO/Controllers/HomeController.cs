using InfoTrack.Tools.Domain.Interfaces;
using InfoTrack.Tools.SEO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using InfoTrack.Tools.Domain.Models;
using Microsoft.Extensions.Primitives;

namespace InfoTrack.Tools.SEO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchEngineService _searchEngineService;

        public HomeController(ILogger<HomeController> logger, ISearchEngineService searchEngineService)
        {
            _logger = logger;
            _searchEngineService = searchEngineService;
        }

        public IActionResult Index()
        {
            return View(new SearchResultViewModel());
        }

        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchResultViewModel searchResultViewModel, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", searchResultViewModel);
            }

            var requestParameter = new SearchRequestParameter(searchResultViewModel.SearchCriteriaKeywords, searchResultViewModel.SearchCriteriaUrl);
            var searchResponseResult = await _searchEngineService.SearchAsync(requestParameter, ct);

            if (searchResponseResult.IsFailure)
                return RedirectToAction("Error");

            searchResultViewModel.ResultRanks = searchResponseResult.Value.ResultPositions;
       
           return View("Index", searchResultViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
