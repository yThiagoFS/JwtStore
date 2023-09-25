namespace Jwt.Core.Contexts.SharedContext.Exceptions
{
    public  class EmailException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "Invalid E-mail";

        public EmailException(string message = DEFAULT_ERROR_MESSAGE)
            : base(message)
        {
        }

    }
}
