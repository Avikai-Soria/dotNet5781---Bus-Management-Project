using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BadLineStationIdException : Exception
    {
        public string error_msg;

        public BadLineStationIdException()
        {
        }

        public BadLineStationIdException(string message) : base(message)
        {
        }

        public BadLineStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadLineStationIdException(Guid lineId, Guid stationId, string v)
        {
            error_msg = v + ": " + lineId + ", " + stationId;
        }

        protected BadLineStationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}