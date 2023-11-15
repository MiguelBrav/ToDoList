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
    public class InstructionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InstructionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This method is to get translated instructions
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{languageId}")]
        public async Task<IActionResult> GetInstructionsTranslated([FromRoute] string languageId)
        {
            if(languageId == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            InstructionCommand command = new InstructionCommand { LanguageId = languageId };

            ApiResponse responseInstructions = await _mediator.Send(command);

            if (responseInstructions.Response == null || responseInstructions.Response is false)
                return StatusCode(responseInstructions.StatusCode, responseInstructions.Response);

            return StatusCode(responseInstructions.StatusCode, JsonConvert.DeserializeObject<List<InstructionsTranslated>>(responseInstructions.ResponseMessage));
        }

        /// <summary>
        /// This method is to get one translated instruction by id
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{instructionId}/{languageId}")]
        public async Task<IActionResult> GetInstructionsTranslatedById([FromRoute] int instructionId, string languageId)
        {
            if (languageId == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            InstructionByIdCommand command = new InstructionByIdCommand { InstructionId = instructionId, LanguageId = languageId };

            ApiResponse responseInstruction = await _mediator.Send(command);

            if (responseInstruction.Response == null || responseInstruction.Response is false)
                return StatusCode(responseInstruction.StatusCode, responseInstruction.Response);

            return StatusCode(responseInstruction.StatusCode, JsonConvert.DeserializeObject<InstructionsTranslated>(responseInstruction.ResponseMessage));
        }
    }
}
