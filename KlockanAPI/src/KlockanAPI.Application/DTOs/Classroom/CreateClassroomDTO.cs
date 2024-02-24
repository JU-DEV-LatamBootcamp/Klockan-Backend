﻿namespace KlockanAPI.Application.DTOs.Classroom;

public class CreateClassroomDTO
{
    public DateOnly StartDate { get; set; }
    public int ProgramId { get; set; }
    public int CourseId { get; set; }
    public List<UpdateClassroomScheduleDTO> Schedule { get; set; } = new List<UpdateClassroomScheduleDTO>();
}
