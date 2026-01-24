using MediatR;
using Microsoft.AspNetCore.Identity;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;
using UseCaseCore.UseCases;

namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class CleanTasksCommandHandler : UseCaseBase<CleanTasksCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersAppService _usersAppService;

        private readonly ITaskUserService _taskUserService;

        private readonly ITaskUserHistoricalService _taskHistoricalUserService;
        public CleanTasksCommandHandler(UserManager<IdentityUser> userManager, IUsersAppService usersAppService, ITaskUserService taskUserService,ITaskUserHistoricalService taskHistoricalUserService)
        {
            _userManager = userManager;
            _usersAppService = usersAppService;
            _taskUserService = taskUserService;
            _taskHistoricalUserService = taskHistoricalUserService;
        }

        public override async Task<ApiResponse> Execute(CleanTasksCommand request)
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

            List<TaskByUser> taskToDeleted = await _taskUserService.GetTasksBinByUserId(request.UserId, request.TaskIds);

            if(taskToDeleted != null)
            {
                List<TaskByUserHistorical> taskToLog = new List<TaskByUserHistorical>();

                Array.ForEach(taskToDeleted.ToArray(), taskUser =>
                {
                    TaskByUserHistorical taskLog = new TaskByUserHistorical();

                    taskLog.UserAppId = taskUser.UserAppId;
                    taskLog.DeleteddUserId = request.UserId;
                    taskLog.DeletedDate = DateTime.UtcNow;
                    taskLog.TaskName = taskUser.TaskName;
                    taskLog.OriginalTaskId = taskUser.Id;
                    taskLog.OriginalTaskTierId = taskUser.OriginalTaskTierId;
                    taskLog.TaskDescription = taskUser.TaskDescription;

                    taskToLog.Add(taskLog);
                });

                bool areTasksLog  = await _taskHistoricalUserService.SaveHistoricalUserTask(taskToLog);

                bool areTasksCleaned = await _taskUserService.CleanTasksBinByUserId(taskToDeleted);

                 response.Response = areTasksCleaned;
                 response.ResponseMessage = "Tasks were cleaned";
                 response.StatusCode = StatusCodes.Status200OK;

                return response;
            }

            response.Response = false;
            response.ResponseMessage = "Tasks were does not exists";
            response.StatusCode = StatusCodes.Status204NoContent;


            return response;
        }
    }
}
