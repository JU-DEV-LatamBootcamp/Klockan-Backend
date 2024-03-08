
namespace KlockanAPI.Application.DTOs.Meeting;

public class CreateMultipleMeetingsDto
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int ClassroomId { get; set; }
    public int TrainerId { get; set; }
    public ICollection<int> Users { get; set; }
}
