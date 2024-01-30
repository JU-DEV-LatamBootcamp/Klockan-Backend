using KlockanAPI.Application.DTOs.Course;

namespace KlockanAPI.Application.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
    Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
}