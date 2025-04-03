using System.Text.Json;
using PricingEngine.Models;

namespace PricingEngine.Repositories;

public class JsonTrainRepository:ITrainRepository
{
    private readonly string _filePath;
    private readonly List<Train> _trains;

    public JsonTrainRepository(string filePath)
    {
        _filePath = filePath;
        _trains = LoadData();
    }

    private List<Train> LoadData()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Train>>(json)?? new List<Train>();
        }
        return new List<Train>();
    }
    

    public  Task<Train?> GetTrainByNumberAsync(string trainNumber)
    {
        return Task.FromResult( _trains.FirstOrDefault(t => t.TrainNumber == trainNumber));
    }

    
}