namespace KlockanAPI.Domain.Models;

public class Role(string name) : BaseModel
{
    public string Name { get; set; } = name;

}
