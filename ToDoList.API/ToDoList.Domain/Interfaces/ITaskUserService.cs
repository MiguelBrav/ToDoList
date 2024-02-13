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
        Task<List<TaskByUser>> GetTasksByUserBin(string userId, int pageId, int sizeId, int taskTierId, int orderById);
        Task<TaskByUser> GetTaskByIdAndUser(string userId, int taskId);
        Task<bool> CancelTasksByUserAndIds(string userId, int[] tasksIds);
        Task<bool> CancelAllTasksByUser(string userId);
        Task<bool> UpdateTask(TaskByUser userTask);
        Task<bool> RestoreTasksByUserAndIds(string userId, int[] tasksIds);
        Task<bool> RestoreAllTasksByUser(string userId);

    }
}
