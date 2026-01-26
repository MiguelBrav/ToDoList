using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IUserAppAggregator
{
    // Commands
    public Task<ApiResponse> UpdateUserAppInfoCommand(UpdateUserAppInfoCommand request);
    // Queries
    public Task<ApiResponse> GetUserAppInfoQuery(GetUserAppInfoQuery request);


}
