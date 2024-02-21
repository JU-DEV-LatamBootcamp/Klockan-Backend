﻿using MapsterMapper;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.Schedule;



namespace KlockanAPI.Application.Services;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public ClassroomService(IClassroomRepository classroomRepository, IMapper mapper)
    {
        _classroomRepository = classroomRepository;
        _mapper = mapper;
    }

    public async Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO)
    {
        var classroom = _mapper.Map<Classroom>(createClassroomDTO);
        var createdClassroom = await _classroomRepository.CreateClassroomAsync(classroom);

        return _mapper.Map<ClassroomDTO>(createdClassroom);
    }


    public async Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync()
    {
        var classrooms = await _classroomRepository.GetAllClassroomsAsync();
        return _mapper.Map<IEnumerable<ClassroomDTO>>(classrooms);
    }

    public List<CreateScheduleDTO> MapCreateClassroomSchedulesDTOsToCreateScheduleDTOs(int id, List<CreateClassroomScheduleDTO> classroomSchedules)
    {
        var schedules = classroomSchedules.Aggregate(
            new List<CreateScheduleDTO>(),
            (schedules, createClassroomSchedule) =>
            {
                var newSchedule = new CreateScheduleDTO(
                    createClassroomSchedule.WeekdayId,
                    id,
                    createClassroomSchedule.StartTime,
                    createClassroomSchedule.FinishTime
                );
                schedules.Add(newSchedule);

                return schedules;
            }
        );

        return schedules;
    }
}

