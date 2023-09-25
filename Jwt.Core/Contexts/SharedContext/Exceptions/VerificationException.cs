namespace Jwt.Core.Contexts.SharedContext.Exceptions
{
    public class VerificationException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "An error ocorred while trying to verify the code. Please, wait a few minutes and try again.";

        public VerificationException(string message = DEFAULT_ERROR_MESSAGE)   : base(message)
        { }
    }
}
