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

        public async Task<List<TaskByUser>> GetTasksByUserBin(string userId, int pageId, int sizeId, int taskTierId, int orderById)
        {
            List<TaskByUser> result = await _dbContext.TaskByUser
                            .FromSqlRaw("EXEC [dbo].[GetTasksByUserIdBin] @UserId, @TaskTierId, @OrderBy, @PageSize, @PageNumber ",
                                new SqlParameter("@UserId", userId),
                                new SqlParameter("@PageNumber", pageId),
                                new SqlParameter("@PageSize", sizeId),
                                new SqlParameter("@TaskTierId", taskTierId),
                                new SqlParameter("@OrderBy", orderById))
                            .ToListAsync();

            return result;
        }


        public async Task<bool> CancelAllTasksByUser(string userId)
        {
            try
            {
                var sql = "UPDATE [dbo].[TaskByUser] SET IsDeleted = 1 WHERE IsDeleted = 0 AND CreatedUserId = @userId";

                await _dbContext.Database.ExecuteSqlRawAsync(sql, new SqlParameter("@userId", userId));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TaskByUser> SaveUserTask(TaskByUser userTask)
        {
            await _dbContext.TaskByUser.AddAsync(userTask);

            await _dbContext.SaveChangesAsync();

            return userTask;
        }

        public async Task<TaskByUser> GetTaskByIdAndUser(string userId, int taskId)
        {
            return await _dbContext.TaskByUser.Where(x => x.Id == taskId && x.CreatedUserId == userId && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateTask(TaskByUser userTask)
        {
            try
            {
                _dbContext.TaskByUser.Update(userTask);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RestoreTasksByUserAndIds(string userId, int[] tasksIds)
        {
            try
            {
                _dbContext.TaskByUser
                    .Where(task => task.IsDeleted && task.CreatedUserId == userId && tasksIds.Contains(task.Id))
                    .ToList()
                    .ForEach(task => task.IsDeleted = false);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> RestoreAllTasksByUser(string userId)
        {
            try
            {
                var sql = "UPDATE [dbo].[TaskByUser] SET IsDeleted = 0 WHERE IsDeleted = 1 AND CreatedUserId = @userId";

                await _dbContext.Database.ExecuteSqlRawAsync(sql, new SqlParameter("@userId", userId));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TaskByUser>> GetTasksBinByUserId(string userId, int[] tasksIds)
        {
            return await _dbContext.TaskByUser
                    .Where(x => tasksIds.Contains(x.Id) && x.CreatedUserId == userId && x.IsDeleted)
                    .ToListAsync();
            
        }

        public async Task<List<TaskByUser>> GetAllTasksBinByUserId(string userId)
        {
            return await _dbContext.TaskByUser
                    .Where(x => x.CreatedUserId == userId && x.IsDeleted)
                    .ToListAsync();

        }

        public async Task<List<TaskByUser>> GeAllTasksByUserId(string userId)
        {
            return await _dbContext.TaskByUser
                    .Where(x => x.CreatedUserId == userId && !x.IsDeleted)
                    .ToListAsync();

        }

        public async Task<bool> CleanTasksBinByUserId(List<TaskByUser> tasksToClean)
        {
            try
            {
                _dbContext.TaskByUser.RemoveRange(tasksToClean);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
