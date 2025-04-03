namespace PricingEngine.Models;

public class Train
{
    public string TrainNumber { get; set; }
    public string TrainType { get; set; }
    public double PricePerHop { get; set; }
    public List<string> Route { get; set; }
    
}