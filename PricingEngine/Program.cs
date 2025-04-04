using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PricingEngine.Models;
using PricingEngine.Repositories;
using PricingEngine.Services;


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


var service = new ServiceCollection();

// Register the configuration

service.AddSingleton<IConfiguration>(config);
service.AddSingleton<ITrainRepository, JsonTrainRepository>();
service.AddSingleton< StandardTicketService>();
service.AddSingleton< DiscountedTicketService>();

//get path from appsettings.json



// call the service
var serviceProvider = service.BuildServiceProvider();


StandardTicketService ticketService = serviceProvider.GetRequiredService<StandardTicketService>();
DiscountedTicketService discountedTicketService = serviceProvider.GetRequiredService<DiscountedTicketService>();

var ticketRequest = new TicketRequest
{
    TrainNumber = "T1",
    From = "A",
    To = "C",
    NumberOfPassengers = 10
};


var price = ticketService.CalculateTicketPriceAsync(ticketRequest).Result;

Console.WriteLine($"Price: {price}");

var discountedPrice = discountedTicketService.CalculateTicketPriceAsync(ticketRequest).Result;

Console.WriteLine($"Discounted Price: {discountedPrice}");