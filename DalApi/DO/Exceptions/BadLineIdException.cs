using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadLineIdException : Exception
    {
        public Guid id;
        public string v;

        public BadLineIdException()
        {
        }

        public BadLineIdException(string message) : base(message)
        {
        }

        public BadLineIdException(Guid id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadLineIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadLineIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}