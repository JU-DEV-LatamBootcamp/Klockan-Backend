using FluentValidation;
using KlockanAPI.Application.DTOs.Program;

namespace KlockanAPI.Application.Validators;

public class CreateProgramDTOValidator : AbstractValidator<CreateProgramDTO>
{
    public CreateProgramDTOValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The name is required.")
            .Length(1, 200).WithMessage("The name must be between 1 and 200 characters.");
    }
}

