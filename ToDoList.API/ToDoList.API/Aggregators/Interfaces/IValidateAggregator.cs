using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IValidateAggregator
{
    // Commands
    public Task<ApiResponse> ValidateTokenCommand(ValidateTokenCommand request);
}
