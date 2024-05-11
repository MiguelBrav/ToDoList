using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands.AdminCommands
{
    public class UserAdminCommand : IRequest<ApiResponse>
    {
        public string Email { get; set; }
        public string KeyAdmin { get; set; }
    }
}
