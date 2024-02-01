using FluentValidation;

namespace KlockanAPI.Application;

public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The name is required.")
            .Length(1, 200).WithMessage("The name must be between 1 and 200 characters.");
        RuleFor(c => c.Sessions)
            .NotEmpty().WithMessage("The amount of sessions is required");
        RuleFor(c => c.SessionDuration)
            .NotEmpty().WithMessage("The duration of sessions is required");
    }
}
