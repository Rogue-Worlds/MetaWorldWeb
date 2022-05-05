using System;
using System.Runtime.Serialization;

namespace MetaWorldWeb.Exceptions
{
    /// <summary>
    /// An exception raised when the requested set of World Hashes failoed to be generated correctly
    /// </summary>
    public class WorldHashException : Exception
    {
        public WorldHashException()
        {
        }

        public WorldHashException(string message) : base(message)
        {
        }

        public WorldHashException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorldHashException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
