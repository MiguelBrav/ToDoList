using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class InstructionByIdQuery : IRequest<ApiResponse>
    {
        public int InstructionId { get; set; }
        public string LanguageId { get; set; }
    }
}
