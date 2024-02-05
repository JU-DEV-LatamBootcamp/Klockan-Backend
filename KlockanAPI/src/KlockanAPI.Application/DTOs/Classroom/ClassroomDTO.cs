namespace KlockanAPI.Application.DTOs.Classroom;

// TODO: update embeded entities
public class ClassroomDTO
{

    public DateOnly StartDate { get; set; }
    public int CourseId { get; set; }
    // public Course? Course { get; set; }
    public int ProgramId { get; set; }
    // public Program? Program { get; set; }
    // public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    // public ICollection<Schedule> Schedule { get; set; } = new List<Schedule>();
    // public ICollection<ClassroomUser> ClassroomUsers { get; set; } = new List<ClassroomUser>();
}
