using System;

namespace PrintCenter.Domain.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}