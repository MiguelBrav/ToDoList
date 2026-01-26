using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class ValidateAggregator : IValidateAggregator
{
    // commands
    private readonly ValidateTokenCommandHandler _validateTokenCommandHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public ValidateAggregator(UseCaseDispatcher useCaseDispatcher, ValidateTokenCommandHandler validateTokenCommandHandler)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _validateTokenCommandHandler = validateTokenCommandHandler;
    }

    public async Task<ApiResponse> ValidateTokenCommand(ValidateTokenCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_validateTokenCommandHandler, request);
    }
}
