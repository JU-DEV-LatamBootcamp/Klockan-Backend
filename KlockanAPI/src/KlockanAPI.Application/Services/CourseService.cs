using MapsterMapper;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.CrossCutting;

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
    public async Task<CourseDto?> DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.DeleteCourseAsync(id);
        NotFoundException.ThrowIfNull(course, $"Course with id {id} not found");

        // check if course is not used in any classroom
        //var classroom = await _classroomRepository.GetClassroomByCourseIdAsync(id);

        return _mapper.Map<CourseDto>(course);
    }
}