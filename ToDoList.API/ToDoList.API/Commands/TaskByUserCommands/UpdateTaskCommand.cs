using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class UpdateTaskCommand 
    {
        public TaskUpdateDTO UpdateTask { get; set; }
        public string UserId { get; set; }
    }
}
