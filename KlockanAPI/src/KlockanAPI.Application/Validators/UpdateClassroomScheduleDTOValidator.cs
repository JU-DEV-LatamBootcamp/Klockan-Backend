using FluentValidation;
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application;

public class UpdateClassroomScheduleDTOValidator : AbstractValidator<UpdateClassroomScheduleDTO>
{
    public UpdateClassroomScheduleDTOValidator()
    {
        RuleFor(t => t.WeekdayId)
            .NotNull()
            .NotEqual(0);

        RuleFor(t => t.StartTime)
            .NotNull();
    }
}