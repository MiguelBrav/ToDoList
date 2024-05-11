using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands.ReportCommands
{
    public class GetUserTasksExcelCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
        public string LanguageId { get; set; }  
    }
}
