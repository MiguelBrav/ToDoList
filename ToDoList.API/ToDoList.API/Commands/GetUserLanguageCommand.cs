using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class GetUserLanguageCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
	}
}
