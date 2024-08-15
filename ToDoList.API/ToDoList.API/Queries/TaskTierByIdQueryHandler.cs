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

namespace ToDoList.API.Queries
{
    public class TaskTierByIdQueryHandler : IRequestHandler<TaskTierByIdQuery, ApiResponse>
    {
        private readonly ITaskTierTranslatedService _taskTierTranslatedService;

        public TaskTierByIdQueryHandler(ITaskTierTranslatedService taskTierTranslatedService)
        {
            _taskTierTranslatedService = taskTierTranslatedService;
        }

        public async Task<ApiResponse> Handle(TaskTierByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            TaskTierTranslated taskTierTranslated = await _taskTierTranslatedService.GetTaskTranslatedById(request.TaskTierId, request.LanguageId);

            if (taskTierTranslated == null)
            {
                response.Response = false;
                response.ResponseMessage = "Error while retrieving the information.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(taskTierTranslated);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
