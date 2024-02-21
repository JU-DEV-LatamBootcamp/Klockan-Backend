
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);

    public List<CreateScheduleDTO> MapCreateClassroomSchedulesDTOsToCreateScheduleDTOs(int id, List<CreateClassroomScheduleDTO> classroomSchedules);
}
