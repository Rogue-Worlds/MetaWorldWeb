using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetaWorldWeb.Resolvers
{
    public interface IContentResolver
    {
        Task<string> GetContentAsync(string url);

        Task<string> GetContentAsync(string url, IDictionary<string, string> formData);

        Task<string> PostContentAsync(string url, IDictionary<string, string> formData);
    }
}