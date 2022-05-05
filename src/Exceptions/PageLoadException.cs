using System;
using System.Runtime.Serialization;

namespace MetaWorldWeb.Exceptions
{
    /// <summary>
    /// A Page Load Exception has occurred. This can mean the Url does not exist or no data was returned.
    /// </summary>
    public class PageLoadException : Exception
    {
        public PageLoadException()
        {
        }

        public PageLoadException(string message) : base(message)
        {
        }

        public PageLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PageLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
