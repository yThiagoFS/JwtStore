namespace Jwt.Core.Contexts.SharedContext.Exceptions
{
    public class VerificationException : Exception
    {
        private const string DefaultErrorMessage = "An error ocorred while trying to verify the code. Please, wait a few minutes and try again.";

        public VerificationException(string message = DefaultErrorMessage)   : base(message)
        { }
    }
}
