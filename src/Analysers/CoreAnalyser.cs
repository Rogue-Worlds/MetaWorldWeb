using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using MetaWorldWeb.Hashing;
using MetaWorldWeb.Models;
using MetaWorldWeb.Models.Internal;

namespace MetaWorldWeb.Analysers
{
    internal class CoreAnalyser : IAnalyser
    {
        private readonly IHash _hasher;

        private readonly IList<string> _tags = new List<string>()
        {
            "//div",
            "//span",
            "//h1",
            "//h2",
            "//p",
            "//tr",
        };

        public CoreAnalyser(IHash hasher)
        {
            _hasher = hasher;
        }

        public ProcessingState Analyse(ref HtmlDocument html, string clientIndentifier, MetaWorldData data)
        {
            data.PageSHA256Hash = _hasher.GenerateRawHash(html.DocumentNode.OuterHtml);

            var titleNode = html.DocumentNode.SelectSingleNode("//title");
            data.Title = titleNode != null ? titleNode.InnerText : string.Empty;

            for (var i = 0; i < data.Hashes.Length; i++)
            {
                if (data.Hashes[i] == 0)
                {
                    data.Hashes[i] = GetHash(html, i + 1);
                }
            }

            return new ProcessingState
            {
                Data = data,
                Continue = true
            };
        }

        private ulong GetHash(HtmlDocument html, int depth)
        {
            HtmlNode htmlTag = null;
            var explored = 0;
            foreach (var tag in _tags)
            {
                var htmlTags = html.DocumentNode.SelectNodes(tag);
                var tagName = htmlTags?.FirstOrDefault()?.Name;

                var matchedNodes = htmlTags?.Where(d => d.InnerHtml.Trim().Length > 0 && d.Descendants().All(c => c.Name != tagName)).Take(depth - explored).ToList();
                explored += matchedNodes.Count;

                htmlTag = explored == depth ? matchedNodes.LastOrDefault() : null;

                if (htmlTag != null)
                {
                    break;
                }
            }

            return htmlTag != null ? _hasher.GenerateHash(htmlTag.OuterHtml) : 0;
        }
    }
}
