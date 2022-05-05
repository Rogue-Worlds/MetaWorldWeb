using HtmlAgilityPack;
using MetaWorldWeb.Models;
using MetaWorldWeb.Models.Internal;

namespace MetaWorldWeb.Analysers
{
    public interface IAnalyser
    {
        ProcessingState Analyse(ref HtmlDocument html, string clientIdentifier, MetaWorldData data);
    }
}
