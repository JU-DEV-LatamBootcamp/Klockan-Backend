using FluentValidation;
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application;

public class CreateClassroomScheduleDTOValidator : AbstractValidator<CreateClassroomScheduleDTO>
{
    public CreateClassroomScheduleDTOValidator()
    {
        RuleFor(t => t.WeekdayId)
            .NotNull()
            .NotEqual(0);
        RuleFor(t => t.StartTime)
            .NotNull();
        RuleFor(t => t.FinishTime)
            .NotNull();
    }
}