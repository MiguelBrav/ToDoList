using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class TaskByUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskByUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This method is to add task by user
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("add")]
        public async Task<IActionResult> SaveTaskByUser([FromBody] TaskDTO newTask)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                CreateTaskCommand taskCommand = new CreateTaskCommand()
                {
                    UserId = userId,
                    Task = newTask
                };

                ApiResponse responseTasl = await _mediator.Send(taskCommand);

                if (responseTasl.Response == null || responseTasl.Response is false)
                    return StatusCode(responseTasl.StatusCode, responseTasl.Response);

                return StatusCode(responseTasl.StatusCode, JsonConvert.DeserializeObject<int>(responseTasl.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }
    }
}
