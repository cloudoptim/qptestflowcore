using System;
using System.Runtime.Serialization;

namespace QPCore.Helpers
{
    public class TokenException : Exception
    {
        public TokenException()
        {
        }

        public TokenException(string message) : base(message)
        {
        }

        public TokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}