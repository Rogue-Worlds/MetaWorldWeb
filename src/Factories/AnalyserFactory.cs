using MetaWorldWeb.Analysers;
using MetaWorldWeb.Hashing;
using System.Collections.Generic;

namespace MetaWorldWeb.Factories
{
    internal class AnalyserFactory : IAnalyserFactory
    {
        private readonly IHash _hasher;

        public AnalyserFactory(IHash hasher)
        {
            _hasher = hasher;
        }

        public IEnumerable<IAnalyser> GetAnalysers()
        {
            var analysers = new List<IAnalyser>
            {
                new MetaAnalyser(),
                new CoreAnalyser(_hasher),
                new LinkAnalyser(),
                new FormAnalyser(),
                new WordAnalyser(),
                new ImageAnalyser(),
            };

            return analysers;
        }
    }
}
