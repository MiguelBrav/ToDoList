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
    public class InstructionsController : ControllerBase
    {
        private readonly IInstructionAggregator _aggregator;
        public InstructionsController(IInstructionAggregator aggregator)
        {
            _aggregator = aggregator;
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

            InstructionQuery command = new InstructionQuery { LanguageId = languageId };

            ApiResponse responseInstructions = await _aggregator.InstructionQuery(command);

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

            InstructionByIdQuery command = new InstructionByIdQuery { InstructionId = instructionId, LanguageId = languageId };

            ApiResponse responseInstruction = await _aggregator.InstructionByIdQuery(command);

            if (responseInstruction.Response == null || responseInstruction.Response is false)
                return StatusCode(responseInstruction.StatusCode, responseInstruction.Response);

            return StatusCode(responseInstruction.StatusCode, JsonConvert.DeserializeObject<InstructionsTranslated>(responseInstruction.ResponseMessage));
        }
    }
}
