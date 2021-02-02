using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadLineTripIdException : Exception
    {
        public Guid lineId;
        public string v;

        public BadLineTripIdException()
        {
        }

        public BadLineTripIdException(string message) : base(message)
        {
        }

        public BadLineTripIdException(Guid lineId, string v)
        {
            this.lineId = lineId;
            this.v = v;
        }

        public BadLineTripIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadLineTripIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}