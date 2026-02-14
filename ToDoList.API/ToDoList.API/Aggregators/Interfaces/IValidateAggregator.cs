using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IValidateAggregator
{
    // Commands
    public Task<ApiResponse> ValidateTokenQuery(ValidateTokenQuery request);
}
