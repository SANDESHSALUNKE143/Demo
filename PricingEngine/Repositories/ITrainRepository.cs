using PricingEngine.Models;

namespace PricingEngine.Repositories;

public interface ITrainRepository
{
    Task<Train?> GetTrainByNumberAsync(string trainNumber);
}