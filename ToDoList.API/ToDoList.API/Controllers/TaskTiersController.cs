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
    public class TaskTiersController : ControllerBase
    {
        private readonly ITaskTierAggregator _aggregator;
        public TaskTiersController(ITaskTierAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is to get translated tasks tier
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{languageId}")]
        public async Task<IActionResult> GetTaskTiersTranslated([FromRoute] string languageId)
        {

            if(languageId == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            TaskTierQuery command = new TaskTierQuery { LanguageId = languageId };

            ApiResponse responseTaskTiers = await _aggregator.TaskTierQuery(command);

            if (responseTaskTiers.Response == null || responseTaskTiers.Response is false)
                return StatusCode(responseTaskTiers.StatusCode, responseTaskTiers.Response);

            return StatusCode(responseTaskTiers.StatusCode, JsonConvert.DeserializeObject<List<TaskTierTranslated>>(responseTaskTiers.ResponseMessage));
        }

        /// <summary>
        /// This method is to get one translated task tier by id
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{tasktierId}/{languageId}")]
        public async Task<IActionResult> GetTaskTierTranslatedById([FromRoute] int tasktierId, string languageId)
        {
            if (languageId == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            TaskTierByIdQuery command = new TaskTierByIdQuery { TaskTierId = tasktierId, LanguageId = languageId };

            ApiResponse responseInstruction = await _aggregator.TaskTierByIdQuery(command);

            if (responseInstruction.Response == null || responseInstruction.Response is false)
                return StatusCode(responseInstruction.StatusCode, responseInstruction.Response);

            return StatusCode(responseInstruction.StatusCode, JsonConvert.DeserializeObject<TaskTierTranslated>(responseInstruction.ResponseMessage));
        }
    }
}
