using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class GetUserAppInfoCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
	}
}
