using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
    Task<CourseDTO> CreateCourseAsync(CreateCourseDTO createCourseDTO);
    Task<CourseDTO?> DeleteCourseAsync(int id);
    Task<CourseDTO> UpdateCourseAsync(Course course);
}

