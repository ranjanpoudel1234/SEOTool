using Newtonsoft.Json;

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google
{
    public class SearchResultDto
    {
        public const string ResultListKey = "organic_results";

        [JsonProperty("position")]
        public int Order { get; set; }

        [JsonProperty("link")]
        public string Url { get; set; }

        [JsonProperty("displayed_link")]
        public string DisplayedUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }
    }
}
