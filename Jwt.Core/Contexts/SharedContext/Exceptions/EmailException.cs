namespace Jwt.Core.Contexts.SharedContext.Exceptions
{
    public  class EmailException : Exception
    {
        private const string DefaultErrorMessage = "Invalid E-mail";

        public EmailException(string message = DefaultErrorMessage)
            : base(message)
        {
        }

    }
}
