using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.AspNetUsers;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;

namespace ToDoList.Infraestructure.Repositories
{
    public class TaskTierTranslatedService : ITaskTierTranslatedService
    {
        private readonly AppDBContext _dbContext;

        public TaskTierTranslatedService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskTierTranslated>> GetTasksTranslated(string languageId)
        {
            return await _dbContext.TaskTierTranslated.Where(x => x.LanguageId == languageId).ToListAsync();
        }

    }
}
