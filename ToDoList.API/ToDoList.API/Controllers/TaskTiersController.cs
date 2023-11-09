using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class TaskTiersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskTiersController(IMediator mediator)
        {
            _mediator = mediator;
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

            TaskTierCommand command = new TaskTierCommand { LanguageId = languageId };

            ApiResponse responseTaskTiers = await _mediator.Send(command);

            if (responseTaskTiers.Response == null || responseTaskTiers.Response is false)
                return StatusCode(responseTaskTiers.StatusCode, responseTaskTiers.Response);

            return StatusCode(responseTaskTiers.StatusCode, JsonConvert.DeserializeObject<List<TaskTierTranslated>>(responseTaskTiers.ResponseMessage));
        }
    }
}
