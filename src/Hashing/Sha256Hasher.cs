using System.Text;
using System.Security.Cryptography;

namespace MetaWorldWeb.Hashing
{
    /// <summary>
    /// SHA256 based hash generator
    /// </summary>
    public class Sha256Hasher : IHash
    {
        private readonly short[] _bytes = { 0, 2, 4, 6, 8, 10, 12, 14 };
        private readonly SHA256 _sha256;

        public Sha256Hasher()
        {
            _sha256 = SHA256.Create();
        }

        /// <summary>
        /// Generate a unique hash number from the supplied string
        /// </summary>
        /// <param name="content">A string contain content to be used to generate the hash</param>
        /// <returns>A long unique number representing the content</returns>
        public ulong GenerateHash(string content)
        {
            var data = _sha256.ComputeHash(Encoding.UTF8.GetBytes(content));

            return data[_bytes[0]] | (ulong)data[_bytes[1]] << 8 | (ulong)data[_bytes[2]] << 16 | (ulong)data[_bytes[3]] << 24 | (ulong)data[_bytes[4]] << 32 | (ulong)data[_bytes[5]] << 40 | (ulong)data[_bytes[6]] << 48 | (ulong)data[_bytes[7]] << 56;
        }

        /// <summary>
        /// Generate a unique raw hash number from the supplied string
        /// </summary>
        /// <param name="content">A string contain content to be used to generate the hash</param>
        /// <returns>A unique byte array representing the content</returns>
        public byte[] GenerateRawHash(string content)
        {
            return _sha256.ComputeHash(Encoding.UTF8.GetBytes(content));
        }
    }
}
