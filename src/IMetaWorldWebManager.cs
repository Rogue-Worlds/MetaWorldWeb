using System.Threading.Tasks;
using MetaWorldWeb.Models;

namespace MetaWorldWeb
{
    /// <summary>
    /// Meta World Web Manager class.
    /// This represents the main entry point to the page interpreter. This can be instantiated via DI or via the MetaWorldWebFactory static class.
    /// </summary>
    public interface IMetaWorldWebManager
    {
        /// <summary>
        /// Retrieve a Meta World interpretation of a web page including a requested number of page hashes, using the HTTP GET verb.
        /// </summary>
        /// <param name="uri">The Uri of the web page to access</param>
        /// <param name="clientIdentifier">A string identifier of the client accessing the page. This allows the interpreter to selectively pick out specific configurations targeted at the client that have been embedded into the webpage.</param>
        /// <param name="hashRequirement">The number of page hashes to return from the page</param>
        /// <returns>A MetaWorldData class representing the accessed page, or Null if the request was invalid</returns>
        Task<MetaWorldData> GetMetaWorldDataAsync(string uri, string clientIdentifier, int hashRequirement);

        /// <summary>
        /// Retrieve a Meta World interpretation of a web page including a requested number of page hashes, using the HTTP POST verb.
        /// </summary>
        /// <param name="formData">The Uri of the web page to access</param>
        /// <param name="clientIdentifier">A string identifier of the client accessing the page. This allows the interpreter to selectively pick out specific configurations targeted at the client that have been embedded into the webpage.</param>
        /// <param name="hashRequirement">The number of page hashes to return from the page</param>
        /// <returns>A MetaWorldData class representing the accessed page, or Null if the request was invalid</returns>
        Task<MetaWorldData> GetMetaWorldDataAsync(FormData formData, string clientIdentifier, int hashRequirement);
    }
}