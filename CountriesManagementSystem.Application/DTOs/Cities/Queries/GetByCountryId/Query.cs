using FluentValidation;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Application.DTOs.Cities.Queries.GetByCountryId
{
    public class Query : SearchListDto
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
