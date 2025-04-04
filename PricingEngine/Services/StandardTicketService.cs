using PricingEngine.Models;
using PricingEngine.Repositories;

namespace PricingEngine.Services;

public class StandardTicketService: BaseTicketService
{
    private readonly ITrainRepository _trainRepository;

    public StandardTicketService(ITrainRepository trainRepository): base(trainRepository)
    {
        _trainRepository = trainRepository;
    }
    
    public override async Task<double> CalculateTicketPriceAsync(TicketRequest ticketRequest)
    {
        
        
        // Get the train by train number
        var train = await _trainRepository.GetTrainByNumberAsync(ticketRequest.TrainNumber);
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
        return train.PricePerHop * hops * ticketRequest.NumberOfPassengers;
    }
}