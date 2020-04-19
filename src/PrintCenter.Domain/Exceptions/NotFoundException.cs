using System;

namespace PrintCenter.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(string name) : base($"{nameof(T)} '{name}' not found.")
        {
        }
    }
}