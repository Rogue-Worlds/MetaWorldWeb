using MetaWorldWeb.Factories;
using MetaWorldWeb.Hashing;
using MetaWorldWeb.Processors;
using MetaWorldWeb.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace MetaWorldWeb
{
    public static class ServiceCollectionExtensions
    {
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
