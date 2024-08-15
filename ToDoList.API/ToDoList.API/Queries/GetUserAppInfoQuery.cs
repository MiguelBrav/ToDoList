using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries
{
    public class GetUserAppInfoQuery : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
    }
}
