using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IInstructionAggregator
{
    // Queries
    public Task<ApiResponse> InstructionByIdQuery(InstructionByIdQuery request);

    public Task<ApiResponse> InstructionQuery(InstructionQuery request);

}
