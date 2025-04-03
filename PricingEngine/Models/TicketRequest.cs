namespace PricingEngine.Models;

public class TicketRequest
{
    //Add Data Annotations and Fluent Validation
    
    
    public string TrainNumber { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public int NumberOfPassengers { get; set; }
}