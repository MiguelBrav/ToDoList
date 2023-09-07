using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class DeleteUserProfileCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
	}
}
