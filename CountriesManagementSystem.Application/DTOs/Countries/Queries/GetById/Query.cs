using FluentValidation;

namespace CountriesManagementSystem.Application.DTOs.Countries.Queries.GetById
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
        public DateTime CreatedAt { get; set; }
        public List<CityDto> Cities { get; set; } = new List<CityDto>();
    }

    public class CityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
