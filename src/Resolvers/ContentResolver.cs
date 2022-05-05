using MetaWorldWeb.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetaWorldWeb.Resolvers
{
    public class ContentResolver : IContentResolver
    {
        private static HttpClient _client;

        public ContentResolver()
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            _client = _client ?? new HttpClient(handler);
        }

        public async Task<string> GetContentAsync(string url)
        {
            try
            {
                var response = await _client.GetStringAsync(url);
                return response;
            }
            catch (NullReferenceException nex)
            {
                throw new PageLoadException($"Page {url} not available or returned no data.", nex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> PostContentAsync(string url, IDictionary<string, string> formData)
        {
            try
            {
                var content = formData != null ? new FormUrlEncodedContent(formData) : null;
                var response = await _client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (NullReferenceException nex)
            {
                throw new PageLoadException($"Page {url} not available or returned no data.", nex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
