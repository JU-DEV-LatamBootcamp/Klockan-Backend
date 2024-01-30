using MapsterMapper;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);
        await _courseRepository.CreateAsync(course);
        
        return _mapper.Map<CourseDto>(course);
    }
}