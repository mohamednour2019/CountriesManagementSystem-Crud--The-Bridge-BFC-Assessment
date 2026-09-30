using FluentValidation;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Application.DTOs.Cities.Queries.GetPagedList
{
    public class Query : SearchListDto
    {
        public string? Name { get; set; }
        public long? CountryId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {

    }

    public class Result
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
