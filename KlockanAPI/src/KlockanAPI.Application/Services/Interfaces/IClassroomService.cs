
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);
    List<UpdateScheduleDTO> MapUpdateClassroomSchedulesDTOsToUpdateScheduleDTOs(int id, List<UpdateClassroomScheduleDTO> classroomSchedules);
    Task<ClassroomDTO?> DeleteClassroomAsync(int id);
    List<int> GetIdListOfDeletedSchedules(List<ScheduleDTO> completeList, List<UpdateClassroomScheduleDTO> updatedList);
    Task<ClassroomDTO> UpdateClassroomAsync(UpdateClassroomDTO updateClassroomDTO);
}
