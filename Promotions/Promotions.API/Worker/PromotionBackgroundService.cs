using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Azure.Messaging.ServiceBus;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;


public class PromotionBackgroundService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IConfiguration _configuration;
    private readonly ISet<int> _processedStartedPromotionIds = new HashSet<int>();
    private readonly ISet<int> _processedEndedPromotionIds = new HashSet<int>();

    public PromotionBackgroundService(IServiceProvider provider, IConfiguration configuration)
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
                
                // Retrieve your promotion service and check for promotions
                var promotionService = scope.ServiceProvider.GetRequiredService<IPromotionService>();

                // Get promotions starting tomorrow
                var startingPromotions = await promotionService.GetPromotionsStartingToday();
                if (startingPromotions != null)
                {
                    foreach (var promotion in startingPromotions)
                    {
                        // Check if the message ID has already been processed
                        if (!_processedStartedPromotionIds.Contains(promotion.Id))
                        {
                            await PublishPromotionEvent(promotion,"promotionstarted");

                            // Add the message ID to the set of processed IDs
                            _processedStartedPromotionIds.Add(promotion.Id);
                        }
                    }
                }

                //Get promotions ending soon
                var endingPromotions = await promotionService.GetPromotionsEndingToday();
                if (startingPromotions != null)
                {
                    foreach (var promotion in endingPromotions)
                    {
                        if (!_processedEndedPromotionIds.Contains(promotion.Id))
                        {
                            // Publish "PromotionEnded" event to Azure Service Bus
                            await PublishPromotionEvent(promotion,"promotionended");

                            // Add the message ID to the set of processed IDs
                            _processedEndedPromotionIds.Add(promotion.Id);
                        }
                    }
                }
            }

            // Sleep for a specified interval before checking again (e.g., 1 hour)
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private async Task PublishPromotionEvent(PromotionResponseModel promotion, string queueName)
    {
       
        // Initialize your Azure Service Bus settings
        string serviceBusConnectionString = _configuration.GetConnectionString("ServiceBusConnectionString");
        
        // Create a Service Bus client
        await using var client = new ServiceBusClient(serviceBusConnectionString);
        
        // create the sender
        ServiceBusSender sender = client.CreateSender(queueName);
        
        // Convert the PromotionResponseModel to a JSON string
        string promotionJson = JsonConvert.SerializeObject(promotion);

        // Create a message
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(promotionJson));

        // Send the message to the queue
        await sender.SendMessageAsync(message);
    }
    
}
