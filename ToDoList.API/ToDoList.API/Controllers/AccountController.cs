using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
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


        /// <summary>
        /// This method is for refresh user token.
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("refresh")]
        public async Task<IActionResult> RefreshUserTokenApp()
        {
            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            if (userClaim != null)
            {
                var email = userClaim.Value;

                RefreshTokenQuery taskCommand = new RefreshTokenQuery()
                {
                    Email = email
                };

                ApiResponse responseRefresh = await _mediator.Send(taskCommand);

                if (responseRefresh.Response == null || responseRefresh.Response is false)
                    return StatusCode(responseRefresh.StatusCode, responseRefresh.Response);

                return StatusCode(responseRefresh.StatusCode, JsonConvert.DeserializeObject<ResponseAuth>(responseRefresh.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to add admin role for an user.
        /// </summary>
        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> AddUserRoleAdminApp([FromBody] UserAdminCommand command)
        {

            ApiResponse responseUser = await _mediator.Send(command);

            if (responseUser.Response == null || responseUser.Response is false)
                return StatusCode(responseUser.StatusCode, responseUser.Response);

            return StatusCode(responseUser.StatusCode, responseUser.Response);
        }

        /// <summary>
        /// This method is to delete admin role for an user.
        /// </summary>
        [HttpDelete]
        [Route("admin")]
        public async Task<IActionResult> DeleteUserRoleAdminApp([FromBody] RemoveUserAdminCommand command)
        {

            ApiResponse responseUser = await _mediator.Send(command);

            if (responseUser.Response == null || responseUser.Response is false)
                return StatusCode(responseUser.StatusCode, responseUser.Response);

            return StatusCode(responseUser.StatusCode, responseUser.Response);
        }
    }
}
