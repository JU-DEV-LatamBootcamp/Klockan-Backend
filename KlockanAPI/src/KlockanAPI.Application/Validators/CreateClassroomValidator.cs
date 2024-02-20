using FluentValidation;
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application;

public class CreateClassroomDTOValidator : AbstractValidator<CreateClassroomDTO>
{
    public CreateClassroomDTOValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("The name is required.");
   
    }
}
