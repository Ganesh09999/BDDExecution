namespace BDDExecution.Support
{

    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() { }

        public ElementNotFoundException(string message) : base(message) { }

        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }


    internal class CustomExceptions : Exception
    {
        public CustomExceptions() { }

        public CustomExceptions(string message) : base(message) { }

        public CustomExceptions(string message, Exception innerException) : base(message, innerException) { }

        public CustomExceptions(Exception exception) : base(exception.Message, exception) { }
    }
}
