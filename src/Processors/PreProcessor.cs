using HtmlAgilityPack;
using System;
using System.Text;

namespace MetaWorldWeb.Processors
{
    internal class PreProcessor : IPreProcessor
    {

        public string PreProcessContent(string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(StripWhitespaces(content));

            var comments = doc.DocumentNode.SelectNodes("//comment()");
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    comment.Remove();
                }
            }

            var scripts = doc.DocumentNode.SelectNodes("//script");
            if (scripts != null)
            {
                foreach (var script in scripts)
                {
                    script.Remove();
                }
            }

            return doc.DocumentNode.OuterHtml;
        }

        private string StripWhitespaces(string content)
        {
            var html = new StringBuilder();

            content = content.Replace(Environment.NewLine, "\n");
            var lines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.EndsWith("  ") || line.Equals(" "))
                {
                    html.Append(line.TrimEnd());
                }
                else if (line.StartsWith("  "))
                {
                    html.Append(line.TrimStart());
                }
                else
                {
                    html.Append(line);
                }
            }

            return html.ToString();
        }
    }
}
