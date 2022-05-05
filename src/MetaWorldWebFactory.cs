using MetaWorldWeb.Factories;
using MetaWorldWeb.Hashing;
using MetaWorldWeb.Processors;
using MetaWorldWeb.Resolvers;

namespace MetaWorldWeb
{
    /// <summary>
    /// Base factory class for generating a Meta World Web Manager for systems and users who are unable or unwilling to use an IoC/DI framework
    /// </summary>
    public static class MetaWorldWebFactory
    {
        /// <summary>
        /// Creates a Meta World Web Manager instance for use
        /// </summary>
        /// <returns>A Meta World Web Manager instance/returns>
        public static IMetaWorldWebManager CreateManager()
        {
            var contentResolver = new ContentResolver();
            var preProcessor = new PreProcessor();
            var hasher = HashFactory.GetHashGenerator();
            var analyserFactory = new AnalyserFactory(hasher);

            return new MetaWorldWebManager(contentResolver, preProcessor, analyserFactory);
        }
    }
}
