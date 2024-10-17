using System;

namespace exceptionLibrary
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
