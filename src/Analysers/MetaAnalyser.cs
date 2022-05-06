using System;
using HtmlAgilityPack;
using MetaWorldWeb.Models;
using MetaWorldWeb.Models.Internal;

namespace MetaWorldWeb.Analysers
{
    internal class MetaAnalyser : IAnalyser
    {
        public ProcessingState Analyse(ref HtmlDocument html, string clientIdentifier, MetaWorldData data)
        {
            var metaTags = html.DocumentNode.SelectNodes($"//meta[starts-with(@name,'mww:v1:{clientIdentifier}:')]");
            if (metaTags != null)
            {
                foreach (var metaTag in metaTags)
                {
                    var metaName = metaTag.Attributes["name"].Value.Replace($"mww:v1:{clientIdentifier}:", "");
                    var metaValue = metaTag.Attributes["content"].Value;

                    data = ParseMetaData(data, metaName, metaValue);
                }
            }

            return new ProcessingState
            {
                Data = data,
                Continue = true
            };
        }

        private MetaWorldData ParseMetaData(MetaWorldData data, string name, string value)
        {
            try
            {
                var namespaces = name.Split(':');
                switch (namespaces[0])
                {
                    case "title":
                        data.Title = value;
                        break;
                    case "hash":
                        if (namespaces.Length > 1)
                        {
                            var hashIndex = int.Parse(namespaces[1]);
                            if (hashIndex >= 0 && hashIndex < data.Hashes.Length)
                            {
                                data.Hashes[hashIndex] = ulong.Parse(value);
                            }
                        }
                        else
                        {
                            data.Hashes[0] = ulong.Parse(value);
                        }
                        break;
                    case "words":
                        data.WordList = value.Replace(",", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                }
            }
            catch (Exception) { }

            return data;
        }
    }
}
