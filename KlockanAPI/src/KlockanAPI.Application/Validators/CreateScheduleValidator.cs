using FluentValidation;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Validators
{
    public class CreateScheduleDTOValidator : AbstractValidator<CreateScheduleDTO>
    {
        public CreateScheduleDTOValidator()
        {
        }
    }
}
