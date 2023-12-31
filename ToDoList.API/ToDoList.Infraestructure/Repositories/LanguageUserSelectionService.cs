﻿using Microsoft.EntityFrameworkCore;
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
    public class LanguageUserSelectionService : IUserLanguageSelectionService
    {
        private readonly AppDBContext _dbContext;

        public LanguageUserSelectionService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LanguageUserSelection> GetLanguageUserSelection(string userId)
        {
            return await _dbContext.LanguageUserSelection.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task SaveLanguageUserSelection(LanguageUserSelection userLanguage)
        {
            await _dbContext.LanguageUserSelection.AddAsync(userLanguage);

            await _dbContext.SaveChangesAsync();
        }

          public async Task UpdateLanguageUserSelection(LanguageUserSelection userLanguage)
        {
            _dbContext.LanguageUserSelection.Update(userLanguage);

            await _dbContext.SaveChangesAsync();
        }
    }
}
