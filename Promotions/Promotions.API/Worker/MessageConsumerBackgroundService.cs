using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class MessageConsumerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IConfiguration _configuration;

    public MessageConsumerBackgroundService(IServiceProvider provider, IConfiguration configuration)
    {
        _provider = provider;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _provider.CreateScope())
            {
                // Initialize your Azure Service Bus settings
                string serviceBusConnectionString = _configuration.GetConnectionString("ServiceBusConnectionString");
                
                // Create a Service Bus client
                await using var client = new ServiceBusClient(serviceBusConnectionString);

                // Process messages from both promotionstarted and promotionended queues
                await ProcessQueue(client, "promotionstarted", stoppingToken);
                await ProcessQueue(client, "promotionended", stoppingToken);
            }
            
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private async Task ProcessQueue(ServiceBusClient client, string queueName, CancellationToken stoppingToken)
    {
        // Create a Service Bus receiver for the specified queue
        ServiceBusReceiver receiver = client.CreateReceiver(queueName);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Receive a batch of messages from the queue asynchronously
                var messages = await receiver.ReceiveMessagesAsync(maxMessages: 10); // Adjust the batch size as needed

                if (messages.Count == 0)
                {
                    break;
                }
                var tasks = messages.Select(async message =>
                {
                    try
                    {
                        string messageBody = Encoding.UTF8.GetString(message.Body);

                        if (queueName == "promotionstarted")
                        {
                            Console.WriteLine("Promotion started event : " + messageBody);
                        }
                        else if (queueName == "promotionended")
                        {
                            Console.WriteLine("Promotion ended event : " + messageBody);
                        }

                        // Process the message here

                        // Complete the message to remove it from the queue
                        await receiver.CompleteMessageAsync(message);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log errors)
                        Console.WriteLine("Error processing message: " + ex.Message);

                        // You can choose to abandon or dead-letter the message in case of errors
                        await receiver.AbandonMessageAsync(message);
                    }
                });
                await Task.WhenAll(tasks);
                
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log errors)

            Console.WriteLine("Error processing messages: " + ex.Message);
            throw;
        }
    }
}
