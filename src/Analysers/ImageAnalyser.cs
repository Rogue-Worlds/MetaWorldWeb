using HtmlAgilityPack;
using MetaWorldWeb.Models;
using MetaWorldWeb.Models.Internal;
using System;
using System.Collections.Generic;

namespace MetaWorldWeb.Analysers
{
    internal class ImageAnalyser : IAnalyser
    {
        public ProcessingState Analyse(ref HtmlDocument html, string clientIdentifier, MetaWorldData data)
        {
            var images = new List<ImageData>();
            var baseUri = new Uri(data.Uri);

            var imageTags = html.DocumentNode.SelectNodes("//img[@src]");
            if (imageTags != null)
            {
                foreach (var image in imageTags)
                {
                    var src = image.GetAttributeValue("src", string.Empty).Trim();
                    if (string.IsNullOrEmpty(src))
                    {
                        continue;
                    }
                    var srcLink = new Uri(baseUri, src);
                    var altText = image.GetAttributeValue("alt", string.Empty);
                    images.Add(new ImageData
                    {
                        ImageUri = srcLink.AbsoluteUri,
                        AltText = altText,
                    });

                    image.RemoveAll();
                }
            }

            data.Images = images.ToArray();

            return new ProcessingState
            {
                Data = data,
                Continue = true
            };
        }
    }
}
