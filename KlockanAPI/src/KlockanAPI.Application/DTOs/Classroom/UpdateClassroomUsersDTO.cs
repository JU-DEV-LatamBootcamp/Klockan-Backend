using KlockanAPI.Application.DTOs.ClassroomUser;

namespace KlockanAPI.Application.DTOs.Classroom;

public class UpdateClassroomUsersDTO
{
    public int Id { get; set; }
    public List<UpdateClassroomUserDTO> Users { get; set; } = new List<UpdateClassroomUserDTO>();
}
