﻿namespace KlockanAPI.Application;

public class CreateCourseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Sessions { get; set; }
    public int? SessionDuration { get; set; }
}
