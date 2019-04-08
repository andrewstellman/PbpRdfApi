using System;
namespace PbpRdfApi
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class InvalidDataException : Exception 
    {
        public InvalidDataException(string message) : base(message) { }
    }
}
