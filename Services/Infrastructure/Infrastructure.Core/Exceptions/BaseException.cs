using System;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Core.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class BaseException : ApplicationException
    {
        public BaseException()
        {

        }

        public BaseException(string message) : base(message)
        {

        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
