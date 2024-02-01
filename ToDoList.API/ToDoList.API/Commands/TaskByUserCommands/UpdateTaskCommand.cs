using MediatR;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class UpdateTaskCommand : IRequest<ApiResponse>
    {
        public TaskUpdateDTO UpdateTask { get; set; }
        public string UserId { get; set; }
    }
}
