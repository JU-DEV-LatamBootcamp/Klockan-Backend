﻿
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();

    Task<ScheduleDTO> CreateScheduleAsync(CreateScheduleDTO createScheduleDTO);
}
