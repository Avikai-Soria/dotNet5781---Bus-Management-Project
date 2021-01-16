using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BadAdjStationsException : Exception
    {
        public string error_msg;

        public BadAdjStationsException()
        {
        }

        public BadAdjStationsException(string message) : base(message)
        {
        }

        public BadAdjStationsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadAdjStationsException(Guid? currStation, Guid? nextStation, string v)
        {
            error_msg = v + ": " + currStation + ", " + nextStation;
        }

        protected BadAdjStationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}