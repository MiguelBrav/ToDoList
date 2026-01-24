using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries.ReportQueries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class ReportAggregator : IReportAggregator
{
    // queries
    private readonly GetUserTasksBinExcelQueryHandler _instructionByIdQueryHandler;
    private readonly GetUserTasksExcelQueryHandler _instructionQueryHandler;


    private readonly UseCaseDispatcher _useCaseDispatcher;

    public ReportAggregator(UseCaseDispatcher useCaseDispatcher, GetUserTasksBinExcelQueryHandler instructionByIdQueryHandler, GetUserTasksExcelQueryHandler instructionQueryHandler)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _instructionByIdQueryHandler = instructionByIdQueryHandler;
        _instructionQueryHandler = instructionQueryHandler;
    }

    public async Task<ApiResponse> GetUserTasksBinExcelQuery(GetUserTasksBinExcelQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_instructionByIdQueryHandler, request);
    }

    public async Task<ApiResponse> GetUserTasksExcelQuery(GetUserTasksExcelQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_instructionQueryHandler, request);
    }
}
