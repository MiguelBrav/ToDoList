using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class UserAppAggregator : IUserAppAggregator
{
    // commands
    private readonly UpdateUserAppInfoCommandHandler _updateUserAppInfoCommandHandler;

    // queries
    private readonly GetUserAppInfoQueryHandler _getUserAppInfoQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public UserAppAggregator(UpdateUserAppInfoCommandHandler updateUserAppInfoCommandHandler, GetUserAppInfoQueryHandler getUserAppInfoQueryHandler, UseCaseDispatcher useCaseDispatcher)
    {
        _updateUserAppInfoCommandHandler = updateUserAppInfoCommandHandler;
        _getUserAppInfoQueryHandler = getUserAppInfoQueryHandler;
        _useCaseDispatcher = useCaseDispatcher;
    }

    public async Task<ApiResponse> UpdateUserAppInfoCommand(UpdateUserAppInfoCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_updateUserAppInfoCommandHandler, request);
    }

    public async Task<ApiResponse> GetUserAppInfoQuery(GetUserAppInfoQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_getUserAppInfoQueryHandler, request);
    }
}
