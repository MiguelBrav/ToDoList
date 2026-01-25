using ToDoList.API.Commands.TaskByUserCommands;
using ToDoList.API.Queries.TaskByUserQueries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface ITaskUserAggregator
{
    // Commands
    public Task<ApiResponse> CancelAllTasksCommand(CancelAllTasksCommand request);
    public Task<ApiResponse> CancelTasksCommand(CancelTasksCommand request);
    public Task<ApiResponse> CreateTaskCommand(CreateTaskCommand request);
    public Task<ApiResponse> UpdateTaskCommand(UpdateTaskCommand request);

    // Queries
    public Task<ApiResponse> GetUserTaskQuery(GetUserTaskQuery request);


}
