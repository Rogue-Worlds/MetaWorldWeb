using HtmlAgilityPack;
using MetaWorldWeb.Models;
using MetaWorldWeb.Models.Internal;
using System;
using System.Collections.Generic;

namespace MetaWorldWeb.Analysers
{
    internal class LinkAnalyser : IAnalyser
    {
        public ProcessingState Analyse(ref HtmlDocument html, string clientIdentifier, MetaWorldData data)
        {
            var links = new List<LinkData>();
            var baseUri = new Uri(data.Uri);

            var linkTags = html.DocumentNode.SelectNodes("//a[@href]");
            if (linkTags != null)
            {
                foreach (var link in linkTags)
                {
                    var linkHref = link.GetAttributeValue("href", string.Empty).Trim();
                    if (string.IsNullOrEmpty(linkHref) || linkHref == "#")
                    {
                        continue;
                    }

                    var linkUri = new Uri(baseUri, linkHref);

                    links.Add(new LinkData
                    {
                        LinkUri = linkUri.AbsoluteUri,
                        LinkText = link.InnerText,
                        IsExternal = IsUrlExternal(baseUri, linkUri),
                        IsAnchor = linkUri == baseUri && !string.IsNullOrWhiteSpace(linkUri.Fragment)
                    });

                    link.RemoveAll();
                }
            }

            data.Links = links.ToArray();

            return new ProcessingState
            {
                Data = data,
                Continue = true
            };
        }

        private bool IsUrlExternal(Uri baseUri, Uri uri)
        {
            return uri.Host != baseUri.Host;
        }
    }
}
