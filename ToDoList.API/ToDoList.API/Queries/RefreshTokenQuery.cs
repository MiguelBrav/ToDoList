using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class RefreshTokenQuery 
    {
        public string Email { get; set; }
    }
}
