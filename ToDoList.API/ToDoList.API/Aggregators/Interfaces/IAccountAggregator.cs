using ToDoList.API.Commands;
using ToDoList.API.Commands.AdminCommands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IAccountAggregator
{
    // Commands
    public Task<ApiResponse> CreateUserCommand(CreateUserCommand request);
    public Task<ApiResponse> LoginUserCommand(LoginUserCommand request);
    public Task<ApiResponse> UserAdminCommand(UserAdminCommand request);
    public Task<ApiResponse> RemoveUserAdminCommand(RemoveUserAdminCommand request);

    // Queries
    public Task<ApiResponse> RefreshTokenQuery(RefreshTokenQuery request);


}
