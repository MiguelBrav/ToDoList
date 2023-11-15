using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class InstructionByIdCommand : IRequest<ApiResponse>
    {
        public int InstructionId { get; set; }
        public string LanguageId { get; set; }
    }
}
