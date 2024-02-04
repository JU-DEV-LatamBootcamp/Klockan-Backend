using MapsterMapper;

using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;


namespace KlockanAPI.Application.Services;

public class ClassroomService : IClassroomService
{
    public Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync()
    {
        throw new NotImplementedException();
    }
}

