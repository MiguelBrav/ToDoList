using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface ITaskTierAggregator
{
    // Queries
    public Task<ApiResponse> TaskTierByIdQuery(TaskTierByIdQuery request);

    public Task<ApiResponse> TaskTierQuery(TaskTierQuery request);

}
