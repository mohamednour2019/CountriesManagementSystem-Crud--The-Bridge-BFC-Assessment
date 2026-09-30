using FluentValidation;

namespace CountriesManagementSystem.Application.DTOs.Cities.Commands.Add
{
    public class Command
    {
        public string Name { get; set; }
        public long CountryId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 50).WithMessage("Name must be between {MinLength} and {MaxLength} characters.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("CountryId Is Not Optional");
        }
    }
    public class Result { }
}
