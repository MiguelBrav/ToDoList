using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class GetUserTaskCommandHandler : IRequestHandler<GetUserTasksCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        private readonly ITaskTierTranslatedService _taskTierTranslatedService;

        private readonly IConfiguration _configuration;

        private readonly string _defaultLanguage;
        public GetUserTaskCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService, ITaskTierTranslatedService taskTierTranslatedService, IConfiguration configuration)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
            _taskTierTranslatedService = taskTierTranslatedService;
            _configuration = configuration;
            _defaultLanguage = _configuration.GetValue<string>("DefaultLanguage");
        }

        public async Task<ApiResponse> Handle(GetUserTasksCommand request, CancellationToken cancellationToken)
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

            List<TaskByUser> tasksByUser = await  _taskUserService.GetTasksByUser(request.UserId,request.PageId,request.SizeId,request.TaskTierId,request.OrderById);

            if (tasksByUser == null || tasksByUser.Count == 0)
            {
                response.Response = false;
                response.ResponseMessage = "The user has no tasks created.";
                response.StatusCode = StatusCodes.Status204NoContent;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(tasksByUser);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

