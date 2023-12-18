using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class TaskTierByIdCommand : IRequest<ApiResponse>
    {
        public int TaskTierId { get; set; }
        public string LanguageId { get; set; }
    }
}
