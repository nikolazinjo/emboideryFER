using System;
using System.Runtime.Serialization;

namespace WebAppProject.Logic
{
    [Serializable]
    internal class DuplicateItemExeption : Exception
    {
        public DuplicateItemExeption()
        {
        }

        public DuplicateItemExeption(string message) : base(message)
        {
        }

        public DuplicateItemExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateItemExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
