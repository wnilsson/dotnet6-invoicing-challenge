using System;

namespace Infrastructure.Core.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string GetFullException(this Exception exception)
        {
            return $"{exception.Message}" +
                   (exception.InnerException != null ? $", Inner Exception:  {exception.InnerException.Message}" : string.Empty);
        }
    }
}
