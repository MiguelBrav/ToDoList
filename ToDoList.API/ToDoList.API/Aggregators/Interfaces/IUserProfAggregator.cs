using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IUserProfAggregator
{
    // Commands
    public Task<ApiResponse> UserProfileCommand(UserProfileCommand request);
    public Task<ApiResponse> UpdateUserProfileCommand(UpdateUserProfileCommand request);
    public Task<ApiResponse> DeleteUserProfileCommand(DeleteUserProfileCommand request);

    // Queries
}
