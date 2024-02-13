using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ToDoList.API.Commands.TaskByUserBinCommands;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class RestoreAllTasksCommandHandler : IRequestHandler<RestoreAllTasksCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        public RestoreAllTasksCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
        }

        public async Task<ApiResponse> Handle(RestoreAllTasksCommand request, CancellationToken cancellationToken)
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

            bool taskCanceled = await _taskUserService.RestoreAllTasksByUser(request.UserId);

            response.Response = taskCanceled;
            response.ResponseMessage = "All Tasks were restored";
            response.StatusCode = StatusCodes.Status200OK;


            return response;
        }
    }
}
