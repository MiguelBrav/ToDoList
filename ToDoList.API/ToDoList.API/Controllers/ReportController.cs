using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ToDoList.API.Commands;
using ToDoList.API.Commands.ReportCommands;
using ToDoList.API.Commands.TaskByUserCommands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This method is to get tasks by User in excel report
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("tasks/excel/{languageId}")]
        public async Task<IActionResult> GetTaskTiersExcel(string languageId)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                if (languageId == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var userId = userClaim.Value;

                GetUserTasksExcelCommand taskCommand = new GetUserTasksExcelCommand()
                {
                    UserId = userId,
                    LanguageId = languageId
                };

                ApiResponse responseTasks = await _mediator.Send(taskCommand);

                if (responseTasks.StatusCode == 204)
                    return StatusCode(responseTasks.StatusCode);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.ResponseMessage);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to get tasks by User in excel report
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("tasks/bin/excel/{languageId}")]
        public async Task<IActionResult> GetTaskTiersBinExcel(string languageId)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                if (languageId == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var userId = userClaim.Value;

                GetUserTasksBinExcelCommand taskCommand = new GetUserTasksBinExcelCommand()
                {
                    UserId = userId,
                    LanguageId = languageId
                };

                ApiResponse responseTasks = await _mediator.Send(taskCommand);

                if (responseTasks.StatusCode == 204)
                    return StatusCode(responseTasks.StatusCode);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.ResponseMessage);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }
    }
}
