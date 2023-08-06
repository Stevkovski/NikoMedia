using System;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Text;
using DataAccess.Entities;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using Services.EmailServices;
using Services.Models;

namespace EmailSender
{
    class Program
    {

        //In real world scenario this would be stored in some config or as we talked in the interview probably azure key-vaults

        const string connectionString = "Endpoint=sb://nikomediaservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=LzGJyH3pUF4pNFD8bbHMBCgvXBGNg1Dir+ASbPzlizg=";
        const string queueName = "emailqueue";
        static IQueueClient queueClient;
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            RegisterServices();

            queueClient = new QueueClient(connectionString, queueName);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionRecievedHandler)
            {
                MaxConcurrentCalls = 1, //to process 1 message at a time, this is configurable
                AutoComplete = false 
            
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions); //ova se vika koa ima nov message

            Console.ReadLine();//this is not to close the app but to continue listening

            await queueClient.CloseAsync();
            DisposeServices();
        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            var emailService = _serviceProvider.GetRequiredService<IEmailService>();

            SendMessageModel messageModel = JsonSerializer.Deserialize<SendMessageModel>(jsonString);

            await emailService.SendEmailAsync(messageModel.SendTo, messageModel.Subject, messageModel.Body);

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionRecievedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Something went wrong: {arg.Exception}");
            return Task.CompletedTask;
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IEmailService, EmailService>();

            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null) return;
            if (_serviceProvider is IDisposable disposable) disposable.Dispose();
        }
    }

}
