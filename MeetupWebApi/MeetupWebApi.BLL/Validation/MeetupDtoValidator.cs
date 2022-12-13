using FluentValidation;
using MeetupWebApi.BLL.DTO;

namespace MeetupWebApi.BLL.Validation
{
    public class MeetupDtoValidator: AbstractValidator<MeetupDto>
    {
        public MeetupDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("required");
            RuleFor(x => x.Topic).NotEmpty().NotNull().WithMessage("required");
            RuleFor(x => x.Adress).NotEmpty().NotNull().WithMessage("required");
            RuleFor(x => x.Spending).NotEmpty().NotNull().WithMessage("required");
        }
    }
}