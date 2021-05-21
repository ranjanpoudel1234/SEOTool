﻿using System.ComponentModel.DataAnnotations;

namespace InfoTrack.Tools.SEO.Models
{
    public class SearchViewModel
    {
        [Required]
        public string Keywords { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
