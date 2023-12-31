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
    public class UsersAppService : IUsersAppService
    {
        private readonly AppDBContext _dbContext;

        public UsersAppService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsersApp> GetUserApp(string userId)
        {
            return await _dbContext.UsersApp.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task SaveUsersApps(UsersApp userApp)
        {
            await _dbContext.UsersApp.AddAsync(userApp);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserApp(UsersApp userApp)
        {
            _dbContext.UsersApp.Update(userApp);

            await _dbContext.SaveChangesAsync();
        }
    }
}
