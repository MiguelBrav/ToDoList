using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class TaskTierQuery : IRequest<ApiResponse>
    {
        public string LanguageId { get; set; }
    }
}
