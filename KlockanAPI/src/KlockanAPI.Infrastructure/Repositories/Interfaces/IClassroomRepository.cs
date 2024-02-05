﻿using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IClassroomRepository
{
    Task<IEnumerable<Classroom>> GetAllClassroomsAsync();
    Task<IEnumerable<Classroom>?> GetClassroomsByCourseIdAsync(int courseId);
    Task<IEnumerable<Classroom>?> GetClassroomsByProgramIdAsync(int programId);
}