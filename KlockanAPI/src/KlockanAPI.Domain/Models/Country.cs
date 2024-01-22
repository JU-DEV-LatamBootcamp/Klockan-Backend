namespace KlockanAPI.Domain.Models;

public class Country(string name, string code, City[] cities) : BaseModel
{

    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public City[] Cities { get; set; } = cities;

}
