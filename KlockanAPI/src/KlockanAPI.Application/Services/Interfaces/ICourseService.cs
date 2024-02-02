using KlockanAPI.Application.DTOs.Course;

namespace KlockanAPI.Application.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
    Task<CourseDTO> CreateCourseAsync(CreateCourseDTO createCourseDTO);
    Task<CourseDTO?> DeleteCourseAsync(int id);
}

