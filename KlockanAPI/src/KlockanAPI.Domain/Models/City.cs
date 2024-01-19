namespace KlockanAPI.Domain.Models;

public class City(string name, string code, Country country) : BaseModel
{
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public Country Country { get; set; } = country;
}