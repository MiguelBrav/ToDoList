﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ToDoList.API.Commands;
using ToDoList.API.Commands.TaskByUserCommands;
using ToDoList.API.Queries.TaskByUserQueries;
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

        /// <summary>
        /// This method is to get tasks by User with filters and pagination
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("page/{pageId}/size/{sizeId}/tasktier/{taskTierId}/order/{orderById}")]
        public async Task<IActionResult> GetTaskTiersTranslated([FromRoute] int pageId, int sizeId, int taskTierId, int orderById)
        {

            if (string.IsNullOrEmpty(pageId.ToString()) || string.IsNullOrEmpty(sizeId.ToString()) || string.IsNullOrEmpty(taskTierId.ToString()) || string.IsNullOrEmpty(orderById.ToString()))
                return StatusCode(StatusCodes.Status400BadRequest);

            if(pageId == 0 || sizeId == 0)
                return StatusCode(StatusCodes.Status400BadRequest,"The minimum value for pageId or SizeId is 1");

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                GetUserTaskQuery taskCommand = new GetUserTaskQuery()
                {
                    UserId = userId,
                    TaskTierId = taskTierId,
                    OrderById = orderById,
                    PageId = pageId,
                    SizeId = sizeId
                };

                ApiResponse responseTasks = await _mediator.Send(taskCommand);

                if (responseTasks.StatusCode == 204)
                    return StatusCode(responseTasks.StatusCode);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, JsonConvert.DeserializeObject<List<TaskByUser>>(responseTasks.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to cancel a list of tasks from user by taskIds
        /// </summary>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("cancel/tasks")]
        public async Task<IActionResult> CancelTasksByUser([FromBody] int[] TaskIds)
        {

            if(TaskIds.Length == 0 || TaskIds == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Task ids are not valid");
            }

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                CancelTasksCommand taskCommand = new CancelTasksCommand()
                {
                    UserId = userId,
                    TaskIds = TaskIds
                };

                ApiResponse responseTasks = await _mediator.Send(taskCommand);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.Response);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to cancel all tasks from user 
        /// </summary>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("cancel/all/tasks")]
        public async Task<IActionResult> CancelAllTasksByUser()
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                CancelAllTasksCommand taskCommand = new CancelAllTasksCommand()
                {
                    UserId = userId
                };

                ApiResponse responseTasks = await _mediator.Send(taskCommand);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.Response);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to update a task from user
        /// </summary>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("update")]
        public async Task<IActionResult> UpdateTaskByUser([FromBody] TaskUpdateDTO updateTask)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                UpdateTaskCommand taskCommand = new UpdateTaskCommand()
                {
                    UserId = userId,
                    UpdateTask = updateTask
                };

                ApiResponse responseTask = await _mediator.Send(taskCommand);

                if (responseTask.Response == null || responseTask.Response is false)
                    return StatusCode(responseTask.StatusCode, responseTask.Response);

                return StatusCode(responseTask.StatusCode, responseTask.ResponseMessage);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }
    }
}
