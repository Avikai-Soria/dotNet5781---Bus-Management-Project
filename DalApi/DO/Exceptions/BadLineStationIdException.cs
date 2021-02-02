using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadLineStationIdException : Exception
    {
        public Guid lineId;
        public Guid stationId;
        public string v;

        public BadLineStationIdException()
        {
        }

        public BadLineStationIdException(string message) : base(message)
        {
        }

        public BadLineStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadLineStationIdException(Guid lineId, Guid station, string v)
        {
            this.lineId = lineId;
            this.stationId = station;
            this.v = v;
        }

        protected BadLineStationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}