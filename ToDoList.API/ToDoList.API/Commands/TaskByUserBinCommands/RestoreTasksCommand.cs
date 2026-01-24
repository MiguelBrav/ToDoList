namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class RestoreTasksCommand 
    {
        public int[] TaskIds { get; set; }
        public string UserId { get; set; }
    }
}
