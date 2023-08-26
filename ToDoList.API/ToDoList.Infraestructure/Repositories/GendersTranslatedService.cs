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
    public class GendersTranslatedService : IGendersTranslatedService
    {
        private readonly AppDBContext _dbContext;

        public GendersTranslatedService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GendersTranslated>> GetGendersTranslateds(string languageId)
        {
            return await _dbContext.GendersTranslated.Where(x => x.LanguageId == languageId).ToListAsync();
        }

    }
}
