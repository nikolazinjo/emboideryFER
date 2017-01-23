using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Logic
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
