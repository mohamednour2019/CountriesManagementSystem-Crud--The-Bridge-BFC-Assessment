using Microsoft.AspNetCore.Mvc;
using CountriesManagementSystem.Application.Common.Models;
using CountriesManagementSystem.Application.Services.CityServices;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.API.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly IAddCityService _addCityService;
        private readonly IUpdateCityService _updateCityService;
        private readonly IDeleteCityService _deleteCityService;
        private readonly IGetCityService _getCityService;
        private readonly IGetCitiesListService _getCitiesListService;
        private readonly IGetCitiesByCountryIdService _getCitiesByCountryIdService;

        public CitiesController(
            IAddCityService addCityService,
            IUpdateCityService updateCityService,
            IDeleteCityService deleteCityService,
            IGetCityService getCityService,
            IGetCitiesListService getCitiesListService,
            IGetCitiesByCountryIdService getCitiesByCountryIdService)
        {
            _addCityService = addCityService;
            _updateCityService = updateCityService;
            _deleteCityService = deleteCityService;
            _getCityService = getCityService;
            _getCitiesListService = getCitiesListService;
            _getCitiesByCountryIdService = getCitiesByCountryIdService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCity([FromBody] CountriesManagementSystem.Application.DTOs.Cities.Commands.Add.Command command, CancellationToken cancellationToken)
        {
            var result = await _addCityService.AddCity(command, cancellationToken);
            return ReturnResponse(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCity([FromRoute] long id, [FromBody] Application.DTOs.Cities.Commands.Edit.Command command, CancellationToken cancellationToken)
        {
            var result = await _updateCityService.UpdateCity(
                id,
                command,
                cancellationToken);

            return ReturnResponse(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCity([FromRoute] long id, CancellationToken cancellationToken)
        {

            var result = await _deleteCityService.DeleteCity(new Application.DTOs.Cities.Commands.Delete.Command() { Id = id }, cancellationToken);
            return ReturnResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Application.DTOs.Cities.Queries.GetById.Result>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCity(long id, CancellationToken cancellationToken)
        {
            var query = new CountriesManagementSystem.Application.DTOs.Cities.Queries.GetById.Query { Id = id };
            var result = await _getCityService.GetCity(query, cancellationToken);
            return ReturnResponse(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PageListResult<Application.DTOs.Cities.Queries.GetPagedList.Result>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities([FromQuery] Application.DTOs.Cities.Queries.GetPagedList.Query query, CancellationToken cancellationToken)
        {
            var result = await _getCitiesListService.GetCitiesList(
                query,
                cancellationToken);

            return ReturnResponse(result);
        }

        [HttpGet("by-country/{countryId}")]
        [ProducesResponseType(typeof(ApiResponse<PageListResult<Application.DTOs.Cities.Queries.GetByCountryId.Result>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesByCountryId([FromRoute] long countryId, [FromQuery] Application.DTOs.Cities.Queries.GetByCountryId.Query query, CancellationToken cancellationToken)
        {
            var result = await _getCitiesByCountryIdService.GetCitiesByCountryId(
                countryId,
                query,
                cancellationToken);

            return ReturnResponse(result);
        }
    }
}
