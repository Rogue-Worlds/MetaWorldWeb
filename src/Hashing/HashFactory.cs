namespace MetaWorldWeb.Hashing
{
    /// <summary>
    /// A factoory class for generating an instance of a hash generator
    /// </summary>
    public static class HashFactory
    {
        /// <summary>
        /// Returns a hashing class to be used by the program
        /// </summary>
        public static IHash GetHashGenerator()
        {
            return new Sha256Hasher();
        }
    }
}
