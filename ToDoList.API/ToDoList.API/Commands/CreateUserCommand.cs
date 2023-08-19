using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class CreateUserCommand : IRequest<ApiResponse>
    {
		public string Email { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public int Gender { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
