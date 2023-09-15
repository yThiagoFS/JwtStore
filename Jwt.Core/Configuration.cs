namespace Jwt.Core
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();
        public static DatabaseConfiguration Database { get; set; } = new();
        public static SmtpConfiguration SmtpConfig { get; set; } = new();


        public class DatabaseConfiguration
        {
           public string ConnectionString { get; set; }
        }

        public class SecretsConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;

            public string JwtPrivateKey { get; set; } = string.Empty;

            public string PasswordSaltKey { get; set; } = string.Empty;
        }

        public class SmtpConfiguration
        {
            public int Port { get; set; }

            public string ApiKey { get; set; } = string.Empty;

            public string Server { get; set; } = string.Empty;

            public string SenderEmail { get; set; } = string.Empty;
        }
    }
}
