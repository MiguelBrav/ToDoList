using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Commands.TaskByUserBinCommands;
using ToDoList.API.Queries;
using ToDoList.API.Queries.TaskByUserBinQueries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class TaskUserBinAggregator : ITaskUserBinAggregator
{
    // commands
    private readonly CleansAllTasksCommandHandler _cleansAllTasksCommandHandler;
    private readonly CleanTasksCommandHandler _cleanTasksCommandHandler;
    private readonly RestoreAllTasksCommandHandler _restoreAllTasksCommandHandler;
    private readonly RestoreTasksCommandHandler _restoreTasksCommandHandler;

    // queries
    private readonly GetUserTaskBinQueryHandler _getUserTaskBinQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public TaskUserBinAggregator(CleansAllTasksCommandHandler cleansAllTasksCommandHandler, CleanTasksCommandHandler cleanTasksCommandHandler, RestoreAllTasksCommandHandler restoreAllTasksCommandHandler, RestoreTasksCommandHandler restoreTasksCommandHandler, GetUserTaskBinQueryHandler getUserTaskBinQueryHandler, UseCaseDispatcher useCaseDispatcher)
    {
        _cleansAllTasksCommandHandler = cleansAllTasksCommandHandler;
        _cleanTasksCommandHandler = cleanTasksCommandHandler;
        _restoreAllTasksCommandHandler = restoreAllTasksCommandHandler;
        _restoreTasksCommandHandler = restoreTasksCommandHandler;
        _getUserTaskBinQueryHandler = getUserTaskBinQueryHandler;
        _useCaseDispatcher = useCaseDispatcher;
    }

    public async Task<ApiResponse> CleansAllTasksCommand(CleansAllTasksCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_cleansAllTasksCommandHandler, request);
    }

    public async Task<ApiResponse> CleanTasksCommand(CleanTasksCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_cleanTasksCommandHandler, request);
    }

    public async Task<ApiResponse> RestoreAllTasksCommand(RestoreAllTasksCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_restoreAllTasksCommandHandler, request);
    }

    public async Task<ApiResponse> RestoreTasksCommand(RestoreTasksCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_restoreTasksCommandHandler, request);
    }
    public async Task<ApiResponse> GetUserTaskBinQuery(GetUserTaskBinQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_getUserTaskBinQueryHandler, request);
    }
}
