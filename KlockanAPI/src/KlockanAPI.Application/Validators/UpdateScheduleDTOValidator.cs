using FluentValidation;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application;

public class UpdateScheduleDTOValidator : AbstractValidator<UpdateScheduleDTO>
{
    public UpdateScheduleDTOValidator()
    {
        RuleFor(t => t.WeekdayId)
            .NotNull()
            .NotEqual(0);

        RuleFor(t => t.StartTime)
            .NotNull();
    }
}
