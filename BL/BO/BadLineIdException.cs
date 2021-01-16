using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BadLineIdException : Exception
    {
        public string error_msg;

        public BadLineIdException()
        {
        }

        public BadLineIdException(string message) : base(message)
        {
        }

        public BadLineIdException(Guid id, string v)
        {
            this.error_msg = v + ": " + id;
        }

        public BadLineIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadLineIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}