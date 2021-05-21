namespace InfoTrack.Tools.Domain.Models
{
    public class SearchRequestParameter
    {
        public SearchRequestParameter(string keywords, string url)
        {
            Keywords = keywords;
            Url = url;
        }

        public string Keywords { get; set; }
        public string Url { get; set; }
    }
}
