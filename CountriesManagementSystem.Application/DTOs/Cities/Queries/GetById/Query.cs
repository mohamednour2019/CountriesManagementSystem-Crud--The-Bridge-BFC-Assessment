using FluentValidation;

namespace CountriesManagementSystem.Application.DTOs.Cities.Queries.GetById
{
    public class Query
    {
        public long Id { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Is Not Optional");
        }
    }

    public class Result
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
