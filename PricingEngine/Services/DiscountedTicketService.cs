using PricingEngine.Models;
using PricingEngine.Repositories;

namespace PricingEngine.Services;

public class DiscountedTicketService: BaseTicketService
{

    public DiscountedTicketService(ITrainRepository trainRepository) : base(trainRepository)
    {
    }

    public override Task<double> CalculateTicketPriceAsync(TicketRequest ticketRequest)
    {
        
        //validate train number
        
        var train = GetTrainByNumber(ticketRequest.TrainNumber);
        
        // Get the train by train number
        if (train == null)
        {
            throw new ArgumentException("Train not found");
        }
        
        var fromIndex = train.Route.IndexOf(ticketRequest.From);
        var toIndex = train.Route.IndexOf(ticketRequest.To);
        if (fromIndex == -1 || toIndex == -1)
        {
            throw new ArgumentException("Invalid route");
        }
        
        if (fromIndex > toIndex)
        {
            throw new ArgumentException("Invalid route");
        }
        
        var hops = toIndex - fromIndex;
        var price = train.PricePerHop * hops * ticketRequest.NumberOfPassengers;
        
        // Apply discount if applicable
        if (ticketRequest.NumberOfPassengers > 5)
        {
            price *= 0.9; // 10% discount for more than 5 passengers
        }
        
        return Task.FromResult(price);
    }
}