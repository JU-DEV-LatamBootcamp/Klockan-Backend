using FluentValidation;
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application;

public class CreateClassroomDTOValidator : AbstractValidator<CreateClassroomDTO>
{
    public CreateClassroomDTOValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("Start Date is required.");

    }
}
