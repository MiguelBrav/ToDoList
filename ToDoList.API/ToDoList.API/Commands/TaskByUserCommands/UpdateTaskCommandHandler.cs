using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        private readonly IConfiguration _configuration;

        public UpdateTaskCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService, IConfiguration configuration)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            IdentityUser userExists = await _userManager.FindByIdAsync(request.UserId);

            if (userExists == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            UsersApp userInformation = await _usersAppService.GetUserApp(userExists.Id);

            if (userInformation == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            TaskByUser existsTask = await _taskUserService.GetTaskByIdAndUser(request.UserId,request.UpdateTask.TaskId);

            if(existsTask == null)
            {
                response.Response = false;
                response.ResponseMessage = "The task does not exists";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            if (AreTasksDifferent(existsTask, request.UpdateTask))
            {
                if (!string.IsNullOrEmpty(request.UpdateTask.TaskName) && existsTask.TaskName != request.UpdateTask.TaskName)
                {
                    existsTask.TaskName = request.UpdateTask.TaskName;
                }

                if (!string.IsNullOrEmpty(request.UpdateTask.TaskDescription) && existsTask.TaskDescription != request.UpdateTask.TaskDescription)
                {
                    existsTask.TaskDescription = request.UpdateTask.TaskDescription;
                }

                if (existsTask.ExpectedDateTime != request.UpdateTask.ExpectedDateTime)
                {
                    existsTask.ExpectedDateTime = request.UpdateTask.ExpectedDateTime;
                }

                if (request.UpdateTask.IsCompleted)
                {
                    existsTask.IsCompleted = true;
                    existsTask.FinishDate = existsTask.FinishDate is null ? DateTime.Now : existsTask.FinishDate;

                } else
                {
                    existsTask.IsCompleted = false;
                    existsTask.FinishDate = null;
                }

                bool resultUpdate = await _taskUserService.UpdateTask(existsTask);

                response.Response = resultUpdate;
                response.ResponseMessage = JsonConvert.SerializeObject("Task updated succesfully");
                response.StatusCode = StatusCodes.Status200OK;

                return response;

            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject("Task is the same");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
        private bool AreTasksDifferent(TaskByUser existingTask, TaskUpdateDTO taskToUpdate)
        {
            return existingTask.TaskName != taskToUpdate.TaskName
                || existingTask.TaskDescription != taskToUpdate.TaskDescription
                || existingTask.ExpectedDateTime != taskToUpdate.ExpectedDateTime
                || existingTask.IsCompleted != taskToUpdate.IsCompleted;
        }
    }

}
