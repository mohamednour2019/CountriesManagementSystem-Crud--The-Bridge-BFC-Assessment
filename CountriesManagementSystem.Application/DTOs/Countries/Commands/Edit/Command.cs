using FluentValidation;

namespace CountriesManagementSystem.Application.DTOs.Countries.Commands.Edit
{
    public class Command
    {
        public string Name { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 50).WithMessage("Name must be between {MinLength} and {MaxLength} characters.");
        }
    }
}
