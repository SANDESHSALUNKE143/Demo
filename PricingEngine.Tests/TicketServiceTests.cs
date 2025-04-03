using Moq;
using PricingEngine.Models;
using PricingEngine.Repositories;
using PricingEngine.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace PricingEngine.Tests;

public class TicketServiceTests
{
    
    Mock<ITrainRepository>  _trainRepository;
    TicketService _ticketService;
    public TicketServiceTests()
    {
        _trainRepository = new Mock<ITrainRepository>();
        _ticketService = new TicketService(_trainRepository.Object);
    }
    
    [Fact]
    public void GetPrice_ValidRoute_ReturnsCorrectPrice()
    {
        // Arrange
        var ticketRequest = new TicketRequest
        {
            TrainNumber = "T1",
            From = "A",
            To = "B",
            NumberOfPassengers = 2
        };
        var train = new Train
        {
            TrainNumber = "T1",
            Route = new List<string> {"A", "B"},
            PricePerHop = 10
        };
        _trainRepository.Setup(t => t.GetTrainByNumberAsync("T1")).ReturnsAsync(train);
        
        // Act
        var price = _ticketService.CalculateTicketPriceAsync(ticketRequest).Result;
        
        // Assert
        Assert.Equal(20, price);
    }
    
    [Fact]
    public void GetPrice_ValidRouteWithHops_ReturnsCorrectPrice()
    {
        // Arrange
        var ticketRequest = new TicketRequest
        {
            TrainNumber = "T1",
            From = "A",
            To = "C",
            NumberOfPassengers = 2
        };
        var train = new Train
        {
            TrainNumber = "T1",
            Route = new List<string> {"A", "B","C"},
            PricePerHop = 10
        };
        _trainRepository.Setup(t => t.GetTrainByNumberAsync("T1")).ReturnsAsync(train);
        
        // Act
        var price = _ticketService.CalculateTicketPriceAsync(ticketRequest).Result;
        
        // Assert
        Assert.Equal(40, price);
    }

    [Fact]
    public void GetPrice_InvalidRoute_ThrowsException()
    {
        
        // Arrange
        var ticketRequest = new TicketRequest
        {
            TrainNumber = "T1",
            From = "A",
            To = "C",
            NumberOfPassengers = 2
        };
        var train = new Train
        {
            TrainNumber = "T1",
            Route = new List<string> {"A", "B"},
            PricePerHop = 10
        };
        _trainRepository.Setup(t => t.GetTrainByNumberAsync("T1")).ReturnsAsync(train);
        
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _ticketService.CalculateTicketPriceAsync(ticketRequest));
        
        // Assert
        Assert.Equal("Invalid route", ex.Result.Message);
        
    }
}