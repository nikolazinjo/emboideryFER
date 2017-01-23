using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp.Logic
{
    [Serializable]
    public class UserAccessDeniedException : Exception
    {
        public UserAccessDeniedException()
        {
        }

        public UserAccessDeniedException(string message) : base(message)
        {
        }

        public UserAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
