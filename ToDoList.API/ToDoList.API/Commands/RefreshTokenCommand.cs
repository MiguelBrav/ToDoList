using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class RefreshTokenCommand : IRequest<ApiResponse>
    {
		public string Email { get; set; }
	}
}
