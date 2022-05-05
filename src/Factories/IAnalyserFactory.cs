using System.Collections.Generic;
using MetaWorldWeb.Analysers;

namespace MetaWorldWeb.Factories
{
    public interface IAnalyserFactory
    {
        IEnumerable<IAnalyser> GetAnalysers();
    }
}