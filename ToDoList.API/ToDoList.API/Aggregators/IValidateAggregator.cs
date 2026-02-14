using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class ValidateAggregator : IValidateAggregator
{
    // queries
    private readonly ValidateTokenQueryHandler _validateTokenQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public ValidateAggregator(UseCaseDispatcher useCaseDispatcher, ValidateTokenQueryHandler validateTokenCommandHandler)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _validateTokenQueryHandler = validateTokenCommandHandler;
    }

    public async Task<ApiResponse> ValidateTokenQuery(ValidateTokenQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_validateTokenQueryHandler, request);
    }
}
