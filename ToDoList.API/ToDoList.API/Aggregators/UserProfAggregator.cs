using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class UserProfAggregator : IUserProfAggregator
{
    // commands
    private readonly UserProfileCommandHandler _userProfileCommandHandler;
    private readonly UpdateUserProfileCommandHandler _updateUserProfileCommandHandler;
    private readonly DeleteUserProfileCommandHandler _deleteUserProfileCommandHandler;

    // queries
    private readonly UseCaseDispatcher _useCaseDispatcher;

    public UserProfAggregator(UserProfileCommandHandler userProfileCommandHandler, UpdateUserProfileCommandHandler updateUserProfileCommandHandler, DeleteUserProfileCommandHandler deleteUserProfileCommandHandler, UseCaseDispatcher useCaseDispatcher)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _userProfileCommandHandler = userProfileCommandHandler;
        _updateUserProfileCommandHandler = updateUserProfileCommandHandler;
        _deleteUserProfileCommandHandler = deleteUserProfileCommandHandler;
        _useCaseDispatcher = useCaseDispatcher;
    }

    public async Task<ApiResponse> UserProfileCommand(UserProfileCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_userProfileCommandHandler, request);
    }

    public async Task<ApiResponse> UpdateUserProfileCommand(UpdateUserProfileCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_updateUserProfileCommandHandler, request);
    }

    public async Task<ApiResponse> DeleteUserProfileCommand(DeleteUserProfileCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_deleteUserProfileCommandHandler, request);
    }
}
