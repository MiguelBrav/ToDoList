using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands
{
    public class TaskTierCommandHandler : IRequestHandler<TaskTierCommand, ApiResponse>
    {
        private readonly ITaskTierTranslatedService _taskTierTranslatedService;

        public TaskTierCommandHandler(ITaskTierTranslatedService taskTierTranslatedService)
        {
            _taskTierTranslatedService = taskTierTranslatedService;
        }

        public async Task<ApiResponse> Handle(TaskTierCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<TaskTierTranslated> taskTierTranslateds = await _taskTierTranslatedService.GetTasksTranslated(request.LanguageId);

            if(taskTierTranslateds == null || taskTierTranslateds.Count == 0)
            {
                response.Response = false;
                response.ResponseMessage = "Error while retrieving the information.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(taskTierTranslateds);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
