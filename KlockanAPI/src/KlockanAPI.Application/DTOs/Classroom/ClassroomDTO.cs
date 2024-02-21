using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.DTOs.ClassroomUser;

namespace KlockanAPI.Application.DTOs.Classroom;

public class ClassroomDTO
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public int ProgramId { get; set; }
    public int CourseId { get; set; }
    public ProgramDTO? Program { get; set; }
    public CourseDTO? Course { get; set; }
 
}
