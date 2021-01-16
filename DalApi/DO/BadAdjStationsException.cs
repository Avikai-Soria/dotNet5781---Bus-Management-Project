using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadAdjStationsException : Exception
    {
        public Guid? currStation;
        public Guid? nextStation;
        public string v;

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
            this.currStation = currStation;
            this.nextStation = nextStation;
            this.v = v;
        }

        protected BadAdjStationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}