using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Commands.TaskByUserBinCommands;
using ToDoList.API.Queries;
using ToDoList.API.Queries.TaskByUserBinQueries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface ITaskUserBinAggregator
{
    // Commands
    public Task<ApiResponse> CleansAllTasksCommand(CleansAllTasksCommand request);
    public Task<ApiResponse> CleanTasksCommand(CleanTasksCommand request);
    public Task<ApiResponse> RestoreAllTasksCommand(RestoreAllTasksCommand request);
    public Task<ApiResponse> RestoreTasksCommand(RestoreTasksCommand request);

    // Queries
    public Task<ApiResponse> GetUserTaskBinQuery(GetUserTaskBinQuery request);


}
