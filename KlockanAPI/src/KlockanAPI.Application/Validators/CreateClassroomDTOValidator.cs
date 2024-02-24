using FluentValidation;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application;

public class CreateClassroomDTOValidator : AbstractValidator<CreateClassroomDTO>
{
    public CreateClassroomDTOValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("Start Date is required.");

        RuleFor(c => c.Schedule)
            .Must(schedule => schedule != null && schedule is IList<UpdateScheduleDTO>)
            .WithMessage("Schedule must be an array of Schedule objects.");

        RuleForEach(c => c.Schedule)
            .NotNull()
            .SetValidator(new UpdateScheduleDTOValidator());
    }
}
