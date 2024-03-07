using FluentValidation;
using KlockanAPI.Application.DTOs.Meeting;

namespace KlockanAPI.Application.Validators;

public class CreateMeetingDtoValidator : AbstractValidator<CreateMultipleMeetingsDto>
{
    public CreateMeetingDtoValidator()
    {
        RuleFor(m => m.TrainerId)
            .NotEmpty()
            .WithMessage("The trainer Id is required");
        
        RuleFor(m => m.ClassroomId)
            .NotEmpty()
            .WithMessage("The Classroom Id is required");

        RuleFor(m => m.Date)
            .NotEmpty()
            .WithMessage("The date is required");
        
        RuleFor(m => m.Time)
            .NotEmpty()
            .WithMessage("The time is required");
    }
}
