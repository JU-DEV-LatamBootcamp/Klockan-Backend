using FluentValidation;
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application;

public class UpdateClassroomDTOValidator : AbstractValidator<UpdateClassroomDTO>
{
    public UpdateClassroomDTOValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("Start Date is required.");

        RuleFor(c => c.Schedule)
            .Must(schedule => schedule != null && schedule is IList<UpdateClassroomScheduleDTO>)
            .WithMessage("Schedule must be an array of Schedule objects.");

        RuleForEach(c => c.Schedule)
            .NotNull()
            .SetValidator(new UpdateClassroomScheduleDTOValidator());
    }
}
