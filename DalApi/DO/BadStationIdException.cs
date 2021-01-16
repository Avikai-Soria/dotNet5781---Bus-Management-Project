using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadStationIdException : Exception
    {
        public Guid code;
        public string text;

        public BadStationIdException()
        {
        }

        public BadStationIdException(string message) : base(message)
        {
        }

        public BadStationIdException(Guid code, string v)
        {
            this.code = code;
            this.text = v;
        }

        public BadStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadStationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}