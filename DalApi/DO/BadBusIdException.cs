using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BadBusIdException : Exception
    {
        private int licenseNum;
        private string v;

        public BadBusIdException()
        {
        }

        public BadBusIdException(string message) : base(message)
        {
        }

        public BadBusIdException(int licenseNum, string v)
        {
            this.licenseNum = licenseNum;
            this.v = v;
        }

        public BadBusIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadBusIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}