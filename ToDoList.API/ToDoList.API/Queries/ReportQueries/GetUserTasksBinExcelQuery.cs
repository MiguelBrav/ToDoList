using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries.ReportQueries
{
    public class GetUserTasksBinExcelQuery : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
        public string LanguageId { get; set; }
    }
}
