using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class CreateTaskCommand 
    {
        public TaskDTO Task { get; set; }
        public string UserId { get; set; }
    }
}
