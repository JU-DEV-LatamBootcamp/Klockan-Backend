using FluentValidation;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Application;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserDtoValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(u => u.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(u => u.CityId).NotEmpty().WithMessage("City is required");
        RuleFor(u => u.RoleId).NotEmpty().WithMessage("Role is required");
    }
}
