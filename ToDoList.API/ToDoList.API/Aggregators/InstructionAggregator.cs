using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class InstructionAggregator : IInstructionAggregator
{
    // queries
    private readonly InstructionByIdQueryHandler _instructionByIdQueryHandler;
    private readonly InstructionQueryHandler _instructionQueryHandler;


    private readonly UseCaseDispatcher _useCaseDispatcher;

    public InstructionAggregator(UseCaseDispatcher useCaseDispatcher, InstructionByIdQueryHandler instructionByIdQueryHandler, InstructionQueryHandler instructionQueryHandler)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _instructionByIdQueryHandler = instructionByIdQueryHandler;
        _instructionQueryHandler = instructionQueryHandler;
    }

    public async Task<ApiResponse> InstructionByIdQuery(InstructionByIdQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_instructionByIdQueryHandler, request);
    }

    public async Task<ApiResponse> InstructionQuery(InstructionQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_instructionQueryHandler, request);
    }
}
