using System;

namespace PrintCenter.Domain.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message) : base(message)
        {
        }
    }

    public class DuplicateException<T> : DuplicateException
    {
        public DuplicateException(string name) : base($"{typeof(T).Name} '{name}' already exits.")
        {
        }
    }
}