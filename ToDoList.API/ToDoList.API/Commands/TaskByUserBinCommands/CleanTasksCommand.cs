namespace ToDoList.API.Commands.TaskByUserBinCommands
{
    public class CleanTasksCommand 
    {
        public int[] TaskIds { get; set; }
        public string UserId { get; set; }
    }
}
