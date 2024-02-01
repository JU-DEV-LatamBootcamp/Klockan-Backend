using MapsterMapper;

using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.CrossCutting;

namespace KlockanAPI.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IClassroomRepository classroomRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _classroomRepository = classroomRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }
    public async Task<CourseDto?> DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        NotFoundException.ThrowIfNull(course, $"Course with id {id} not found");

        var classroom = await _classroomRepository.GetClassroomsByCourseIdAsync(id);
        FoundException.ThrowIfNotNull(classroom, $"Course with id {id} is used in a classroom");

        var deletedCourse = await _courseRepository.DeleteCourseAsync(course!);


        return _mapper.Map<CourseDto>(deletedCourse);
    }
}
