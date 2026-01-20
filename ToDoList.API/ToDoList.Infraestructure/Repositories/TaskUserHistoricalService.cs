using ToDoList.Domain.Interfaces;
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
