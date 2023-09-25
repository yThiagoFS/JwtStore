namespace Jwt.Infra.Contexts.SharedContexts.Exceptions
{
    public class EmailServiceException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "Something went wrong while trying to use the email service.";

        public EmailServiceException(string errorMessage = DEFAULT_ERROR_MESSAGE) : base(errorMessage)
        { }
    }
}
