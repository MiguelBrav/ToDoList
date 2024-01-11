using MediatR;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class CancelTasksCommand : IRequest<ApiResponse>
    {
        public int[] TaskIds { get; set; }
        public string UserId { get; set; }
    }
}
