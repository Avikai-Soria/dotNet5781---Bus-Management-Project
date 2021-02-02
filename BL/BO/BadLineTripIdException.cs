using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BadLineTripIdException : Exception
    {
        public string error_msg;

        public BadLineTripIdException()
        {
        }

        public BadLineTripIdException(string message) : base(message)
        {
        }

        public BadLineTripIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public BadLineTripIdException(Guid code, string text)
        {
            this.error_msg = text + ": " + code;
        }

        protected BadLineTripIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}