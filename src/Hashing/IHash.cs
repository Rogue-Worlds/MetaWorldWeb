namespace MetaWorldWeb.Hashing
{
    public interface IHash
    {

        /// <summary>
        /// Generate a unique hash number from the supplied string
        /// </summary>
        /// <param name="content">A string contain content to be used to generate the hash</param>
        /// <returns>A long unique number representing the content</returns>
        ulong GenerateHash(string content);

        /// <summary>
        /// Generate a unique raw hash number from the supplied string
        /// </summary>
        /// <param name="content">A string contain content to be used to generate the hash</param>
        /// <returns>A unique byte array representing the content</returns>
        byte[] GenerateRawHash(string content);
    }
}
