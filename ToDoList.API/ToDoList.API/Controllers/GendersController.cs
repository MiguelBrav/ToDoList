using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class GendersController : ControllerBase
    {
        private readonly IGenderAggregator _aggregator;
        public GendersController(IGenderAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is to get translated genders
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{languageId}")]
        public async Task<IActionResult> GetGendersTranslated([FromRoute] string languageId)
        {

            if(languageId == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            GendersQuery query = new GendersQuery { LanguageId = languageId };

            ApiResponse responseTaskTiers = await _aggregator.GendersQuery(query);

            if (responseTaskTiers.Response == null || responseTaskTiers.Response is false)
                return StatusCode(responseTaskTiers.StatusCode, responseTaskTiers.Response);

            return StatusCode(responseTaskTiers.StatusCode, JsonConvert.DeserializeObject<List<GendersTranslated>>(responseTaskTiers.ResponseMessage));
        }
    }
}
