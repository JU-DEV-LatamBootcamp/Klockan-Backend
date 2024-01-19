namespace KlockanAPI.Domain.Models;

public class Program {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Program(int id, string name, string description) {
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }

}
