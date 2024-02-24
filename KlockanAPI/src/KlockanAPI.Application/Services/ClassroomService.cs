﻿using MapsterMapper;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMapper _mapper;

    public ClassroomService(IClassroomRepository classroomRepository, IMapper mapper, IMeetingRepository meetingRepository, IScheduleRepository scheduleRepository)
    {
        _classroomRepository = classroomRepository;
        _meetingRepository = meetingRepository;
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

    // IMPROVEMENT: implement this method in a mapper profile
    public List<UpdateScheduleDTO> MapUpdateClassroomSchedulesDTOsToUpdateScheduleDTOs(int id, List<UpdateClassroomScheduleDTO> classroomSchedules)
    {
        var schedules = classroomSchedules.Aggregate(
            new List<UpdateScheduleDTO>(),
            (schedules, updateClassroomSchedule) =>
            {
                var newSchedule = new UpdateScheduleDTO(
                    updateClassroomSchedule.Id,
                    updateClassroomSchedule.WeekdayId,
                    id,
                    updateClassroomSchedule.StartTime
                );
                schedules.Add(newSchedule);

                return schedules;
            }
        );

        return schedules;
    }

    public List<int> GetIdListOfDeletedSchedules(List<ScheduleDTO> completeList, List<UpdateClassroomScheduleDTO> updatedList)
    {
        var deletedSchedules = new List<int>();

        foreach (var schedule in completeList)
        {
            if (!updatedList.Any(s => s.Id == schedule.Id))
            {
                deletedSchedules.Add(schedule.Id);
            }
        }

        return deletedSchedules;
    }

    public async Task<ClassroomDTO> UpdateClassroomAsync(UpdateClassroomDTO updateClassroomDTO)
    {
        var id = updateClassroomDTO.Id;
        var dbClassroom = await _classroomRepository.GetClassroomByIdAsync(id);
        NotFoundException.ThrowIfNull(dbClassroom, $"Classroom with id {id} not found");

        var classroom = _mapper.Map<Classroom>(updateClassroomDTO);
        var schedules = _mapper.Map<List<Schedule>>(classroom);

        classroom.Schedule = schedules;

        var udpatedClassroom = await _classroomRepository.UpdateClassroomAsync(classroom);

        return _mapper.Map<ClassroomDTO>(udpatedClassroom);
    }

    public async Task<ClassroomDTO?> DeleteClassroomAsync(int id)
    {
        var classroom = await _classroomRepository.GetClassroomByIdAsync(id);
        NotFoundException.ThrowIfNull(classroom, $"Classroom with id {id} not found");
        var meetings = await _meetingRepository.GetMeetingsByClassroomIdAsync(id);
        FoundException.ThrowIfNotNull(meetings, $"Classroom {id} has meetings assigned ot it.");
        var deletedClassroom = await _classroomRepository.DeleteClassroomAsync(classroom!);
        return _mapper.Map<ClassroomDTO>(deletedClassroom);
    }
}

