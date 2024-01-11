using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;


namespace ToDoList.Domain.Interfaces
{
    public interface ITaskUserService
    {
        Task<TaskByUser> SaveUserTask(TaskByUser userTask);
        Task<List<TaskByUser>> GetTasksByUser(string userId, int pageId, int sizeId, int taskTierId, int orderById);
        Task<bool> CancelTasksByUserAndIds(string userId, int[] tasksIds);
        Task<bool> CancelAllTasksByUser(string userId);
    }
}
