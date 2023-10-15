using Jwt.Core;
using Jwt.Core.Contexts.SharedContext.ValueObjects;
using Jwt.Infra.Contexts.SharedContexts.Services.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Jwt.Infra.Contexts.SharedContexts.Services
{
    public class MessageService : IMessageService
    {
        private readonly string _hostName;

        public MessageService()
        {
            _hostName = Configuration.RabbitMQConfig.HostName;
        }

        public async Task SendMessageAsync<T>(T value) 
        {
            try
            {
                var factory = new ConnectionFactory { HostName = _hostName };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare("EmailVerification", false, false, false, null);

                var messageSerialized = JsonConvert.SerializeObject(value);

                channel.BasicPublish(string.Empty,"EmailVerification", null, Encoding.UTF8.GetBytes(messageSerialized));

                Console.WriteLine($" [x] Sent {messageSerialized}");
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
