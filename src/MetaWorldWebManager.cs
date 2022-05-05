using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Linq;
using MetaWorldWeb.Models;
using MetaWorldWeb.Processors;
using MetaWorldWeb.Factories;
using MetaWorldWeb.Resolvers;
using System;
using MetaWorldWeb.Exceptions;

namespace MetaWorldWeb
{
    /// <summary>
    /// Meta World Web Manager class.
    /// This represents the main entry point to the page interpreter. This can be instantiated via DI or via the MetaWorldWebFactory static class.
    /// </summary>
    internal class MetaWorldWebManager : IMetaWorldWebManager
    {
        private readonly IContentResolver _resolver;
        private readonly IPreProcessor _preProcessor;

        private readonly IAnalyserFactory _analyserFactory;

        public MetaWorldWebManager(IContentResolver resolver, IPreProcessor preProcessor, IAnalyserFactory analyserFactory)
        {
            _resolver = resolver;
            _preProcessor = preProcessor;

            _analyserFactory = analyserFactory;
        }

        /// <summary>
        /// Retrieve a Meta World interpretation of a web page including a requested number of page hashes, using the HTTP GET verb.
        /// </summary>
        /// <param name="uri">The Uri of the web page to access</param>
        /// <param name="clientIdentifier">A string identifier of the client accessing the page. This allows the interpreter to selectively pick out specific configurations targeted at the client that have been embedded into the webpage.</param>
        /// <param name="hashRequirement">The number of page hashes to return from the page</param>
        /// <returns>A MetaWorldData class representing the accessed page, or Null if the request was invalid</returns>
        public async Task<MetaWorldData> GetMetaWorldDataAsync(string uri, string clientIdentifier, int hashRequirement)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (clientIdentifier != null)
            {
                clientIdentifier = clientIdentifier.Trim();
            }
            if (hashRequirement < 0)
            {
                throw new ArgumentException("Invalid value", nameof(hashRequirement));
            }
            
            return await GetContentAndAnalyse(uri, clientIdentifier, hashRequirement);
        }

        /// <summary>
        /// Retrieve a Meta World interpretation of a web page including a requested number of page hashes, using the HTTP POST verb.
        /// </summary>
        /// <param name="formData">The Uri of the web page to access</param>
        /// <param name="clientIdentifier">A string identifier of the client accessing the page. This allows the interpreter to selectively pick out specific configurations targeted at the client that have been embedded into the webpage.</param>
        /// <param name="hashRequirement">The number of page hashes to return from the page</param>
        /// <returns>A MetaWorldData class representing the accessed page, or Null if the request was invalid</returns>
        public async Task<MetaWorldData> GetMetaWorldDataAsync(FormData formData, string clientIdentifier, int hashRequirement)
        {
            _ = formData ?? throw new ArgumentNullException(nameof(formData));
            if (clientIdentifier != null)
            {
                clientIdentifier = clientIdentifier.Trim();
            }
            if (hashRequirement < 0)
            {
                throw new ArgumentException("Invalid value", nameof(hashRequirement));
            }

            return await PostContentAndAnalyse(formData, clientIdentifier, hashRequirement);
        }

        #region Private methods
        private async Task<MetaWorldData> GetContentAndAnalyse(string uri, string clientIdentifier, int hashRequirement)
        {
            var content = await _resolver.GetContentAsync(uri);
            var result = AnalyseContent(uri, content, clientIdentifier, hashRequirement);
            
            result.Uri = uri;

            return result;
        }

        private async Task<MetaWorldData> PostContentAndAnalyse(FormData formData, string clientIdentifier, int hashRequirement)
        {
            if (formData.ActionMethod.ToLowerInvariant() != "post")
            {
                return null;
            }
;
            var dataToPost = formData.Inputs.Concat(formData.Hidden).ToDictionary(x => x.Key, x => x.Value);
            var content = await _resolver.PostContentAsync(formData.ActionUri, dataToPost);

            var result = AnalyseContent(formData.ActionUri, content, clientIdentifier, hashRequirement);
            
            return result;
        }

        private MetaWorldData AnalyseContent(string uri, string content, string clientIdentifier, int hashRequirement)
        {
            _ = content ?? throw new ArgumentNullException(nameof(content));
            
            content = _preProcessor.PreProcessContent(content);

            var worldData = new MetaWorldData
            {
                Hashes = new ulong[hashRequirement],
                Retrieved = DateTime.UtcNow,
                Uri = uri,
            };

            var html = new HtmlDocument();
            html.LoadHtml(content);
            
            var analysers = _analyserFactory.GetAnalysers();
            foreach (var analyser in analysers)
            {
                var state = analyser.Analyse(ref html, clientIdentifier, worldData);
                worldData = state.Data;
                if (!state.Continue)
                {
                    break;
                }
            }

            CheckWorldHashes(worldData.Hashes, hashRequirement);

            return worldData;
        }

        private void CheckWorldHashes(ulong[] hashSet, int hashRequitement)
        {
            if (hashSet == null || hashSet.Length != hashRequitement)
            {
                throw new WorldHashException("World Hash length does not meet client requirements");
            }

            if (hashRequitement > 0)
            {
                var lastHash = hashSet[0];
                if (lastHash == 0)
                {
                    throw new WorldHashException("Failed to generate any World Hashes");
                }

                for (var i = 1; i < hashSet.Length; i++)
                {
                    if(hashSet[i] == 0)
                    {
                        hashSet[i] = lastHash + 1;
                    }
                    lastHash = hashSet[i];
                }
            }
        }
        #endregion
    }
}
