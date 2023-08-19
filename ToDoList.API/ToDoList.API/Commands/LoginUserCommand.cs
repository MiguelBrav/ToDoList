using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class LoginUserCommand : IRequest<ApiResponse>
    {
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
