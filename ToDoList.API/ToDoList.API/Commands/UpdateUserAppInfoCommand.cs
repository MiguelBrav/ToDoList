using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class UpdateUserAppInfoCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
    }
}
