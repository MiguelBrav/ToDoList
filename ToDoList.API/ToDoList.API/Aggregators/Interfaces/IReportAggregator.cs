using ToDoList.API.Queries;
using ToDoList.API.Queries.ReportQueries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Aggregators.Interfaces;

public interface IReportAggregator
{
    // Queries
    public Task<ApiResponse> GetUserTasksBinExcelQuery(GetUserTasksBinExcelQuery request);

    public Task<ApiResponse> GetUserTasksExcelQuery(GetUserTasksExcelQuery request);

}
