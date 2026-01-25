using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Commands.TaskByUserBinCommands;
using ToDoList.API.Commands.TaskByUserCommands;
using ToDoList.API.Queries;
using ToDoList.API.Queries.TaskByUserBinQueries;
using ToDoList.API.Queries.TaskByUserQueries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class TaskUserAggregator : ITaskUserAggregator
{
    // commands
    private readonly CancelAllTasksCommandHandler _cancelAllTasksCommandHandler;
    private readonly CancelTasksCommandHandler _cancelTasksCommandHandler;
    private readonly CreateTaskCommandHandler _createTaskCommandHandler;
    private readonly UpdateTaskCommandHandler _updateTaskCommandHandler;

    // queries
    private readonly GetUserTaskQueryHandler _getUserTaskQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public TaskUserAggregator(CancelAllTasksCommandHandler cancelAllTasksCommandHandler, CancelTasksCommandHandler cancelTasksCommandHandler, CreateTaskCommandHandler createTaskCommandHandler, UpdateTaskCommandHandler updateTaskCommandHandler, GetUserTaskQueryHandler getUserTaskQueryHandler, UseCaseDispatcher useCaseDispatcher)
    {
        _cancelAllTasksCommandHandler = cancelAllTasksCommandHandler;
        _cancelTasksCommandHandler = cancelTasksCommandHandler;
        _createTaskCommandHandler = createTaskCommandHandler;
        _updateTaskCommandHandler = updateTaskCommandHandler;
        _getUserTaskQueryHandler = getUserTaskQueryHandler;
        _useCaseDispatcher = useCaseDispatcher;
    }

    public async Task<ApiResponse> CancelAllTasksCommand(CancelAllTasksCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_cancelAllTasksCommandHandler, request);
    }

    public async Task<ApiResponse> CancelTasksCommand(CancelTasksCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_cancelTasksCommandHandler, request);
    }

    public async Task<ApiResponse> CreateTaskCommand(CreateTaskCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_createTaskCommandHandler, request);
    }

    public async Task<ApiResponse> UpdateTaskCommand(UpdateTaskCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_updateTaskCommandHandler, request);
    }
    public async Task<ApiResponse> GetUserTaskQuery(GetUserTaskQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_getUserTaskQueryHandler, request);
    }
}
