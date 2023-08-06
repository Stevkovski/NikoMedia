using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Queueservice
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _config;

        public QueueService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync<T>(T serviceBusessage, string queueName) //emailqueue
        {
            try
            {
                var queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);
                string messageBody = JsonSerializer.Serialize(serviceBusessage);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                await queueClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                //Would log the error here in real world scenario
                throw;
            }
        }
    }
}
