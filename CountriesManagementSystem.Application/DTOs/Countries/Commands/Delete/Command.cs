using FluentValidation;

namespace CountriesManagementSystem.Application.DTOs.Countries.Commands.Delete
{
    public class Command
    {
        public long Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Is Not Optional");
        }
    }
}
