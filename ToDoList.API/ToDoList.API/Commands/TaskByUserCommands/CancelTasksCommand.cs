namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class CancelTasksCommand
    {
        public int[] TaskIds { get; set; }
        public string UserId { get; set; }
    }
}
