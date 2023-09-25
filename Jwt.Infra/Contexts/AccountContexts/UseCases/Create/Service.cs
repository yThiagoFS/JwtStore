using Jwt.Core;
using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Jwt.Core.Contexts.SharedContext.ValueObjects;
using Jwt.Infra.Contexts.SharedContexts.Exceptions;
using Jwt.Infra.Contexts.SharedContexts.Services.Contracts;
using Polly;
using Polly.Retry;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Jwt.Infra.Contexts.AccountContexts.UseCases.Create
{
    public class Service : IService
    {
        private readonly IMessageService _messageService;

        private const int MAX_RETRY_COUNT = 5;

        private readonly AsyncRetryPolicy<Response> _retryPolicy = Policy
            .HandleResult<Response>(response => !response.IsSuccessStatusCode)
            .WaitAndRetryAsync(MAX_RETRY_COUNT, retryAttempt => TimeSpan.FromSeconds(retryAttempt), (exception, timeSpan, retryCount, context) =>
            {
                if(retryCount == MAX_RETRY_COUNT)
                  throw new EmailServiceException($"Maximum retry attached while trying to send the verification e-mail. Last status code: {exception.Result.StatusCode}");
            });

        public Service(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //TODO: Criar maneira de compensação
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            SendGridMessage message = new SendGridMessage();

            try
            {
                var sendGridClient = new SendGridClient(Configuration.SmtpConfig.ApiKey);

                var from = new EmailAddress(Configuration.SmtpConfig.SenderEmail, "Thiago");

                var to = new EmailAddress(user.Email);

                var htmlContent = CreateEmailHtmlBody(user.Name, user.Email.Verification.Code);

                message = MailHelper.CreateSingleEmail(from, to, "Verification Code", null, htmlContent);

                var result = await _retryPolicy.ExecuteAsync(() => sendGridClient.SendEmailAsync(message));
            }
            catch(Exception ex)
            {
                await _messageService.SendMessageAsync<SendGridMessage>(message);
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
