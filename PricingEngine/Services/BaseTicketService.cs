using PricingEngine.Models;
using PricingEngine.Repositories;

namespace PricingEngine.Services;

public abstract class BaseTicketService:ITicketService
{
    protected readonly ITrainRepository _trainRepository;
    
    protected BaseTicketService(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }
    
    protected Train GetTrainByNumber(string trainNumber)
    {
        var train = _trainRepository.GetTrainByNumberAsync(trainNumber).Result;
        if (train == null)
        {
            throw new ArgumentException("Train not found");
        }
        return train;
    }
    
    public abstract Task<double> CalculateTicketPriceAsync(TicketRequest ticketRequest);
    
    
}