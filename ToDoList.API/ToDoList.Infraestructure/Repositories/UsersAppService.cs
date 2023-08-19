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

        public async Task SaveUsersApps(UsersApp userApp)
        {
            await _dbContext.UsersApp.AddAsync(userApp);

            _dbContext.SaveChanges();
        }

    }
}
