using MediatR;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class RestoreAllTasksCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
    }
}
