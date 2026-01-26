using Newtonsoft.Json;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class TaskTierQueryHandler : UseCaseBase<TaskTierQuery, ApiResponse>
    {
        private readonly ITaskTierTranslatedService _taskTierTranslatedService;

        public TaskTierQueryHandler(ITaskTierTranslatedService taskTierTranslatedService)
        {
            _taskTierTranslatedService = taskTierTranslatedService;
        }

        public override async Task<ApiResponse> Execute(TaskTierQuery request)
        {
            ApiResponse response = new ApiResponse();

            List<TaskTierTranslated> taskTierTranslateds = await _taskTierTranslatedService.GetTasksTranslated(request.LanguageId);

            if (taskTierTranslateds == null || taskTierTranslateds.Count == 0)
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
