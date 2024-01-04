using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.AspNetUsers;
using ToDoList.DTO.UsersApp;

namespace ToDoList.Infraestructure.Repositories
{
    public class TaskUserService : ITaskUserService
    {
        private readonly AppDBContext _dbContext;

        public TaskUserService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CancelTasksByUserAndIds(string userId, int[] tasksIds)
        {
            try
            {
                _dbContext.TaskByUser
                    .Where(task => !task.IsDeleted && task.CreatedUserId == userId && tasksIds.Contains(task.Id))
                    .ToList()
                    .ForEach(task => task.IsDeleted = true);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<List<TaskByUser>> GetTasksByUser(string userId, int pageId, int sizeId, int taskTierId, int orderById)
        {
            List<TaskByUser> result = await _dbContext.TaskByUser
                            .FromSqlRaw("EXEC [dbo].[GetTasksByUserId] @UserId, @TaskTierId, @OrderBy, @PageSize, @PageNumber ",
                                new SqlParameter("@UserId", userId),
                                new SqlParameter("@PageNumber", pageId),
                                new SqlParameter("@PageSize", sizeId),
                                new SqlParameter("@TaskTierId", taskTierId),
                                new SqlParameter("@OrderBy", orderById))
                            .ToListAsync();

            return result;
        }

        public async Task<TaskByUser> SaveUserTask(TaskByUser userTask)
        {
            await _dbContext.TaskByUser.AddAsync(userTask);

            await _dbContext.SaveChangesAsync();

            return userTask;
        }



    }
}
