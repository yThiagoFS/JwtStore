using Jwt.Core;
using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Jwt.Infra.Contexts.AccountContexts.UseCases.Create
{
    public class Service : IService
    {
        //TODO: Criar maneira de compensação
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                var sendGridClient = new SendGridClient(Configuration.SmtpConfig.ApiKey);

                var from = new EmailAddress(Configuration.SmtpConfig.SenderEmail, "Thiago");

                var to = new EmailAddress(user.Email);

                var htmlContent = CreateEmailHtmlBody(user.Name, user.Email.Verification.Code);

                var msg = MailHelper.CreateSingleEmail(from, to, "Verification Code", null, htmlContent);

                await sendGridClient.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
          
        }

        private string CreateEmailHtmlBody(string userName, string code)
            => $@"<html>
                <body>
                    <h3>Hello {userName}, welcome to the JwtStore!</h3>
                    <p>Here's your verification code:<b>{code}</b></p>
                </body>
                </html>";
    }
}
