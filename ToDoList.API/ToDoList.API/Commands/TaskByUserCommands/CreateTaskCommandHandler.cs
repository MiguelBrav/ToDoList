using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        private readonly ITaskTierTranslatedService _taskTierTranslatedService;

        private readonly IConfiguration _configuration;

        private readonly string _defaultLanguage;
        public CreateTaskCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService, ITaskTierTranslatedService taskTierTranslatedService, IConfiguration configuration)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
            _taskTierTranslatedService = taskTierTranslatedService;
            _configuration = configuration;
            _defaultLanguage = _configuration.GetValue<string>("DefaultLanguage");
        }

        public async Task<ApiResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<TaskTierTranslated> taskTierTranslateds = await _taskTierTranslatedService.GetTasksTranslated(_defaultLanguage);

            List<int> taskTiers = taskTierTranslateds.Select(x => x.OriginalTaskTierId).ToList();

            if (!taskTiers.Contains(request.Task.OriginalTaskTierId))
            {
                response.Response = false;
                response.ResponseMessage = "The priority level of task is not valid";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

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


            TaskByUser newTask = new TaskByUser()
            {
                UserAppId = userInformation.Id,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                ExpectedDateTime = request.Task.ExpectedDateTime,
                IsCompleted = false,
                IsDeleted = false,
                LastModificationDate = DateTime.UtcNow,
                TaskName = request.Task.TaskName,
                OriginalTaskTierId = request.Task.OriginalTaskTierId,
                TaskDescription = request.Task.TaskDescription
            };

            newTask = await _taskUserService.SaveUserTask(newTask);

            if (newTask.Id == 0 || newTask.Id == default)
            {
                response.Response = false;
                response.ResponseMessage = "Error while saving the task";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(newTask.Id);
            response.StatusCode = StatusCodes.Status200OK;


            return response;
        }
    }
}
