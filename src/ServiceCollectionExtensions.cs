using MetaWorldWeb.Factories;
using MetaWorldWeb.Hashing;
using MetaWorldWeb.Processors;
using MetaWorldWeb.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace MetaWorldWeb
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register the MetaWorldWeb library dependencies and services in the IoC container Service Collection
        /// </summary>
        /// <param name="sc">Service Collection to add to</param>
        /// <returns>Service Collection</returns>
        public static IServiceCollection AddMetaWorldWeb(this IServiceCollection sc)
        {
            sc.AddTransient<IAnalyserFactory, AnalyserFactory>();

            sc.AddTransient<IHash, Sha256Hasher>();
            sc.AddTransient<IContentResolver, ContentResolver>();
            sc.AddTransient<IPreProcessor, PreProcessor>();
            
            sc.AddTransient<IMetaWorldWebManager, MetaWorldWebManager>();

            return sc;
        }
    }
}
