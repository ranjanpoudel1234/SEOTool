using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoTrack.Tools.SEO.Models
{
    public class SearchResultViewModel
    {
        [Required]
        public string SearchCriteriaKeywords { get; set; }

        [Required]
        [Url]
        public string SearchCriteriaUrl { get; set; }

        public List<int> ResultRanks { get; set; } = new List<int>();
    }
}
