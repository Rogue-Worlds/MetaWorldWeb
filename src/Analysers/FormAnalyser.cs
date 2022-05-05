using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MetaWorldWeb.Models;
using MetaWorldWeb.Models.Internal;

namespace MetaWorldWeb.Analysers
{
    public class FormAnalyser : IAnalyser
    {
        public ProcessingState Analyse(ref HtmlDocument html, string clientIdentifier, MetaWorldData data)
        {
            var baseUri = new Uri(data.Uri);

            var forms = html.DocumentNode.SelectNodes("//form");
            if (forms == null)
            {
                return new ProcessingState
                {
                    Data = data,
                    Continue = true
                };
            }
            var compiledForms = new List<FormData>();
            foreach (var form in forms)
            {
                var actionLinkValue = form.Attributes["action"]?.Value.Trim();
                if (string.IsNullOrEmpty(actionLinkValue))
                {
                    continue;
                }
                var actionLink = new Uri(baseUri, actionLinkValue);
                var formData = new FormData
                {
                    ActionUri = actionLink.AbsoluteUri,
                    ActionMethod = form.Attributes["method"]?.Value ?? "get",
                    Inputs = new Dictionary<string, string>(),
                    Hidden = new Dictionary<string, string>()
                };
                var inputs = form.SelectNodes("//input");
                if (inputs != null)
                {
                    foreach (var input in inputs)
                    {
                        var name = input.Attributes["name"]?.Value ?? input.Attributes["id"]?.Value ?? string.Empty;
                        var value = input.Attributes["value"]?.Value ?? string.Empty;
                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }
                        if (input.Attributes["type"]?.Value.ToLower() == "hidden")
                        {
                            if (!formData.Hidden.ContainsKey(name))
                            {
                                formData.Hidden.Add(name, value);
                            }
                        }
                        else
                        {
                            if (!formData.Inputs.ContainsKey(name))
                            {
                                formData.Inputs.Add(name, value);
                            }
                        }
                    }
                }
                compiledForms.Add(formData);
            }

            data.Forms = compiledForms.ToArray();

            return new ProcessingState
            {
                Data = data,
                Continue = true
            };
        }
    }
}
