using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using InfoTrack.Tools.Domain.Models;

namespace InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google
{
    public static class GoogleSearchResultParser
    {
        public static SearchResponse GetSearchResponseFromHtml(string html, string searchUrl)
        {
            var linkNodeWithPosition = ParseHtml(html);
            var searchResponse = new SearchResponse
            {
                ResultPositions = linkNodeWithPosition
                    .Where(x => x.LinkTag.Contains(searchUrl, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(x => x.IndexPosition)
                    .Select(x => x.IndexPosition)
                    .ToList()
            };

            return searchResponse;
        }

        private static IEnumerable<GoogleSearchResult> ParseHtml(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var linkNodeWithPosition = htmlDocument.DocumentNode.SelectNodes(".//div[@class='kCrYT']")
                .Descendants("a")
                .OrderBy(node => node.LinePosition)
                .Select((row, index) => new GoogleSearchResult { LinkTag = row.GetAttributeValue("href", ""), IndexPosition = index + 1} );


            return linkNodeWithPosition;
        }

        internal class GoogleSearchResult
        {
            public string LinkTag { get; set; }
            public int IndexPosition { get; set; }
        }
    }
}
