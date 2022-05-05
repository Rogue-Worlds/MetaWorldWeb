using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Linq;
using MetaWorldWeb.Models.Internal;
using MetaWorldWeb.Models;

namespace MetaWorldWeb.Analysers
{
    public class WordAnalyser : IAnalyser
    {
        public ProcessingState Analyse(ref HtmlDocument html, string clientIdentifier, MetaWorldData data)
        {
            var result = new List<string>();

            var htmlTags = html.DocumentNode.SelectNodes("//body//text()[normalize-space()]");
            foreach (var tag in htmlTags)
            {
                var words = Regex.Matches(tag.InnerText, @"(?<!\S)[a-zA-Z-]+(?!\S)")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToList();

                result.AddRange(words);
            }
            data.WordList = result.ToArray();

            return new ProcessingState
            {
                Continue = true,
                Data = data
            };
        }
    }
}
