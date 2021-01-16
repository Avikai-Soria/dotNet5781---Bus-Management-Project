using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BadStationIdException : Exception
    {
        public string error_msg;

        public BadStationIdException()
        {
        }

        public BadStationIdException(string message) : base(message)
        {
        }

        public BadStationIdException(Guid code, string text)
        {
            this.error_msg = text + ": " + code;
        }

        public BadStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadStationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}