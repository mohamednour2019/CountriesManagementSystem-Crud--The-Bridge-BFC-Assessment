using Microsoft.AspNetCore.Mvc;
using CountriesManagementSystem.Application.Common.Models;
using CountriesManagementSystem.Application.Services.CountryServices;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.API.Controllers
{
    public class CountriesController : BaseController
    {
        private readonly IAddCountryService _addCountryService;
        private readonly IUpdateCountryService _updateCountryService;
        private readonly IDeleteCountryService _deleteCountryService;
        private readonly IGetCountryService _getCountryService;
        private readonly IGetCountriesListService _getCountriesListService;

        public CountriesController(
            IAddCountryService addCountryService,
            IUpdateCountryService updateCountryService,
            IDeleteCountryService deleteCountryService,
            IGetCountryService getCountryService,
            IGetCountriesListService getCountriesListService)
        {
            _addCountryService = addCountryService;
            _updateCountryService = updateCountryService;
            _deleteCountryService = deleteCountryService;
            _getCountryService = getCountryService;
            _getCountriesListService = getCountriesListService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCountry([FromBody] CountriesManagementSystem.Application.DTOs.Countries.Commands.Add.Command command, CancellationToken cancellationToken)
        {
            var result = await _addCountryService.AddCountry(command, cancellationToken);
            return ReturnResponse(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCountry([FromRoute] long id, [FromBody] Application.DTOs.Countries.Commands.Edit.Command command, CancellationToken cancellationToken)
        {
            var result = await _updateCountryService.UpdateCountry(
                id,
                command,
                cancellationToken);

            return ReturnResponse(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCountry([FromRoute] long id, CancellationToken cancellationToken)
        {

            var result = await _deleteCountryService.DeleteCountry(new Application.DTOs.Countries.Commands.Delete.Command() { Id = id }, cancellationToken);
            return ReturnResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Application.DTOs.Countries.Queries.GetById.Result>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountry(long id, CancellationToken cancellationToken)
        {
            var query = new CountriesManagementSystem.Application.DTOs.Countries.Queries.GetById.Query { Id = id };
            var result = await _getCountryService.GetCountry(query, cancellationToken);
            return ReturnResponse(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PageListResult<Application.DTOs.Countries.Queries.GetPagedList.Result>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountries([FromQuery] Application.DTOs.Countries.Queries.GetPagedList.Query query, CancellationToken cancellationToken)
        {
            var result = await _getCountriesListService.GetCountriesList(
                query,
                cancellationToken);

            return ReturnResponse(result);
        }
    }
}
