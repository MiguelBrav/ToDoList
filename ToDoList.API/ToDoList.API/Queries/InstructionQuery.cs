using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class InstructionQuery : IRequest<ApiResponse>
    {
		public string LanguageId { get; set; }
	}
}
