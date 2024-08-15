using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class TaskTierByIdQuery : IRequest<ApiResponse>
    {
        public int TaskTierId { get; set; }
        public string LanguageId { get; set; }
    }
}
