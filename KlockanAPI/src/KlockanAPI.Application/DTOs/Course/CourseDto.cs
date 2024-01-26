﻿namespace KlockanAPI.Application.DTOs.Course;

public class CourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Sessions { get; set; }
    public int? SessionDuration { get; set; }
}