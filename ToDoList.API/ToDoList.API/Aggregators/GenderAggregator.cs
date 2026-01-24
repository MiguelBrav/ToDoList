using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class GenderAggregator : IGenderAggregator
{
    // queries
    private readonly GendersQueryHandler _gendersQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public GenderAggregator(UseCaseDispatcher useCaseDispatcher, GendersQueryHandler gendersQueryHandler)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _gendersQueryHandler = gendersQueryHandler;

    }

    public async Task<ApiResponse> GendersQuery(GendersQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_gendersQueryHandler, request);
    }
}
