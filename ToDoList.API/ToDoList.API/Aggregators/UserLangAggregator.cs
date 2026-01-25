using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Aggregators;

public class UserLangAggregator : IUserLangAggregator
{
    // commands
    private readonly SaveUserLanguageCommandHandler _saveUserLanguageCommandHandler;
    private readonly UpdateUserLanguageCommandHandler _updateUserLanguageCommandHandler;

    // queries
    private readonly GetUserLanguageQueryHandler _getUserLanguageQueryHandler;

    private readonly UseCaseDispatcher _useCaseDispatcher;

    public UserLangAggregator(SaveUserLanguageCommandHandler saveUserLanguageCommandHandler, UpdateUserLanguageCommandHandler updateUserLanguageCommandHandler, GetUserLanguageQueryHandler getUserLanguageQueryHandler, UseCaseDispatcher useCaseDispatcher)
    {
        _useCaseDispatcher = useCaseDispatcher;
        _saveUserLanguageCommandHandler = saveUserLanguageCommandHandler;
        _updateUserLanguageCommandHandler = updateUserLanguageCommandHandler;
        _getUserLanguageQueryHandler = getUserLanguageQueryHandler;
        _useCaseDispatcher = useCaseDispatcher;
    }

    public async Task<ApiResponse> SaveUserLanguageCommand(SaveUserLanguageCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_saveUserLanguageCommandHandler, request);
    }

    public async Task<ApiResponse> UpdateUserLanguageCommand(UpdateUserLanguageCommand request)
    {
        return await _useCaseDispatcher.Dispatch(_updateUserLanguageCommandHandler, request);
    }

    public async Task<ApiResponse> GetUserLanguageQuery(GetUserLanguageQuery request)
    {
        return await _useCaseDispatcher.Dispatch(_getUserLanguageQueryHandler, request);
    }


}
