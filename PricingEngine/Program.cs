using Microsoft.Extensions.DependencyInjection;
using PricingEngine.Repositories;
using PricingEngine.Services;


var service = new ServiceCollection();
service.AddSingleton<ITrainRepository, JsonTrainRepository>();
service.AddSingleton<TicketService>();



