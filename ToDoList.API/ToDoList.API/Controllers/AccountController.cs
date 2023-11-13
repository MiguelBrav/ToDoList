using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This method is to add a new user.
        /// </summary>
        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUserApp([FromBody] CreateUserCommand command)
        {

            ApiResponse responseUser = await _mediator.Send(command);

            return StatusCode(responseUser.StatusCode, responseUser);
        }

        /// <summary>
        /// This method is for logging in with a user.
        /// </summary>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserApp([FromBody] LoginUserCommand command)
        {

            ApiResponse responseUser = await _mediator.Send(command);

            if (responseUser.Response == null || responseUser.Response is false)
                return StatusCode(responseUser.StatusCode, responseUser.Response);

            return StatusCode(responseUser.StatusCode, JsonConvert.DeserializeObject<ResponseAuth>(responseUser.ResponseMessage));
        }
    }
}
