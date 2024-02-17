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
    public class TaskUserHistoricalService : ITaskUserHistoricalService
    {
        private readonly AppDBContext _dbContext;

        public TaskUserHistoricalService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveHistoricalUserTask(List<TaskByUserHistorical> usersTasks)
        {
            try
            {
                await _dbContext.TaskByUserHistorical.AddRangeAsync(usersTasks);

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
