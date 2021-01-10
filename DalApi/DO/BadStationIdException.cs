using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadStationIdException : Exception
    {
        private int code;
        private string v;

        public BadStationIdException()
        {
        }

        public BadStationIdException(string message) : base(message)
        {
        }

        public BadStationIdException(int code, string v)
        {
            this.code = code;
            this.v = v;
        }

        public BadStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadStationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}