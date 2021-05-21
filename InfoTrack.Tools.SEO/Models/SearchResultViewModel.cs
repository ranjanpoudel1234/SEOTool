using System.Collections.Generic;

namespace InfoTrack.Tools.SEO.Models
{
    public class SearchResultViewModel
    {
        public SearchViewModel SearchCriteria { get; set; }
        public List<int> ResultRanks { get; set; }
    }
}
