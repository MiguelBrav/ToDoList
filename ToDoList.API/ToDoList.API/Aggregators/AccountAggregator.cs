using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class AccountAggregator : IAccountAggregator
{
    // commands
    private readonly CreateUserCommandHandler _createUserCommandHandler;
    private readonly LoginUserCommandHandler _loginUserCommandHandler;
    private readonly UserAdminCommandHandler _userAdminCommandHandler;
    private readonly RemoveUserAdminCommandHandler _removeUserAdminCommandHandler;

    // queries
    private readonly RefreshTokenQueryHandler _refreshTokenQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public AccountAggregator(CreateUserCommandHandler createUserCommandHandler, UseCaseDispatcher useCaseDispatcher, LoginUserCommandHandler loginUserCommandHandler, RefreshTokenQueryHandler refreshTokenQueryHandler, UserAdminCommandHandler userAdminCommandHandler, RemoveUserAdminCommandHandler removeUserAdminCommandHandler)
    {
        _createUserCommandHandler = createUserCommandHandler;
        _useCaseDispatcher = useCaseDispatcher;
        _loginUserCommandHandler = loginUserCommandHandler;
        _refreshTokenQueryHandler = refreshTokenQueryHandler;
        _userAdminCommandHandler = userAdminCommandHandler;
        _removeUserAdminCommandHandler = removeUserAdminCommandHandler;
    }

    public async Task<ApiResponse> CreateUserCommand(CreateUserCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_createUserCommandHandler, request);
    }

    public async Task<ApiResponse> LoginUserCommand(LoginUserCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_loginUserCommandHandler, request);
    }

    public async Task<ApiResponse> RefreshTokenQuery(RefreshTokenQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_refreshTokenQueryHandler, request);
    }

    public async Task<ApiResponse> UserAdminCommand(UserAdminCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_userAdminCommandHandler, request);
    }
    public async Task<ApiResponse> RemoveUserAdminCommand(RemoveUserAdminCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_removeUserAdminCommandHandler, request);
    }
}
