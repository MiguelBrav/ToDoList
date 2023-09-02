using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class UserProfileCommand : IRequest<ApiResponse>
    {
		public string UrlImage { get; set; }
        public string UserId { get; set; }
	}
}
