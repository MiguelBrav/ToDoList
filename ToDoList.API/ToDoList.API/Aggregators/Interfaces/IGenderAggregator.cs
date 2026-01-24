using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IGenderAggregator
{
    // Queries
    public Task<ApiResponse> GendersQuery(GendersQuery request);
}
