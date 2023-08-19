using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class ValidateTokenCommand : IRequest<ApiResponse>
    {
		public string Email { get; set; }
		public string UserId { get; set; }
	}
}
