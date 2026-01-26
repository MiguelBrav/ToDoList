using Microsoft.AspNetCore.Identity;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;
using UseCaseCore.UseCases;

namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class RestoreTasksCommandHandler : UseCaseBase<RestoreTasksCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        public RestoreTasksCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
        }

        public override async Task<ApiResponse> Execute(RestoreTasksCommand request)
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

            bool taskRestored = await _taskUserService.RestoreTasksByUserAndIds(request.UserId, request.TaskIds);

            response.Response = taskRestored;
            response.ResponseMessage = "Tasks were restored";
            response.StatusCode = StatusCodes.Status200OK;


            return response;
        }
    }
}
