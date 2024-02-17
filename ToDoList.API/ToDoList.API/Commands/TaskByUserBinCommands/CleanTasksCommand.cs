using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class CleanTasksCommand : IRequest<ApiResponse>
    {
        public int[] TaskIds { get; set; }
        public string UserId { get; set; }
    }
}
