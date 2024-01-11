using MediatR;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class CancelAllTasksCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
    }
}
