using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class TaskTierAggregator : ITaskTierAggregator
{
    // queries
    private readonly TaskTierByIdQueryHandler _taskTierByIdQueryHandler;
    private readonly TaskTierQueryHandler _taskTierQueryHandler;


    private readonly UseCaseDispatcher _useCaseDispatcher;

    public TaskTierAggregator(UseCaseDispatcher useCaseDispatcher, TaskTierByIdQueryHandler taskTierByIdQueryHandler, TaskTierQueryHandler taskTierQueryHandler)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _taskTierByIdQueryHandler = taskTierByIdQueryHandler;
        _taskTierQueryHandler = taskTierQueryHandler;
    }

    public async Task<ApiResponse> TaskTierByIdQuery(TaskTierByIdQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_taskTierByIdQueryHandler, request);
    }

    public async Task<ApiResponse> TaskTierQuery(TaskTierQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_taskTierQueryHandler, request);
    }
}
