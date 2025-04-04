using PricingEngine.Models;

namespace PricingEngine.Services;

public interface ITicketService
{
    Task<double> CalculateTicketPriceAsync(TicketRequest ticketRequest);
}