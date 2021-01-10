using System;
using System.Runtime.Serialization;

namespace Dal
{
    [Serializable]
    public class BadAdjStationsException : Exception
    {
        private int? currStation;
        private int? nextStation;
        private string v;

        public BadAdjStationsException()
        {
        }

        public BadAdjStationsException(string message) : base(message)
        {
        }

        public BadAdjStationsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadAdjStationsException(int? currStation, int? nextStation, string v)
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