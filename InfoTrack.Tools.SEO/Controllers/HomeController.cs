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
            return View(new SearchViewModel());
        }

        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchViewModel searchViewModel, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", searchViewModel);
            }

            var requestParameter = new SearchRequestParameter(searchViewModel.Keywords, searchViewModel.Url);
            //var result = await _searchEngineService.SearchAsync(requestParameter, ct);


           //todo validate search view model and show the partial view after this.
           return View("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
