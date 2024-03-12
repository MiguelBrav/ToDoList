using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class CleansAllTasksCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
    }
}
