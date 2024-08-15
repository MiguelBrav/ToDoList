using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class RefreshTokenQuery : IRequest<ApiResponse>
    {
        public string Email { get; set; }
    }
}
