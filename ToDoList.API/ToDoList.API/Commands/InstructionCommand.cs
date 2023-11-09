using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class InstructionCommand : IRequest<ApiResponse>
    {
		public string LanguageId { get; set; }
	}
}
