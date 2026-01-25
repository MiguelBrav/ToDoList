using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IUserLangAggregator
{
    // Commands
    public Task<ApiResponse> SaveUserLanguageCommand(SaveUserLanguageCommand request);
    public Task<ApiResponse> UpdateUserLanguageCommand(UpdateUserLanguageCommand request);

    // Queries
    public Task<ApiResponse> GetUserLanguageQuery(GetUserLanguageQuery request);


}
