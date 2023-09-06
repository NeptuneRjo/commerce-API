namespace CommerceApi.CustomExceptions
{
    public class ObjectExistsException : Exception
    {
        public ObjectExistsException() { }

        public ObjectExistsException(string message)
            : base(message) { }

        public ObjectExistsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

