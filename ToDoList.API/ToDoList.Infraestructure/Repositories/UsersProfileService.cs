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
    public class UsersProfileService : IUsersProfileService
    {
        private readonly AppDBContext _dbContext;

        public UsersProfileService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteUserProfile(UserProfile userProfile)
        {
            _dbContext.UserProfile.Remove(userProfile);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<UserProfile> GetUserProfile(string userId)
        {
            return await _dbContext.UserProfile.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task SaveUserProfile(UserProfile userProfile)
        {
            await _dbContext.UserProfile.AddAsync(userProfile);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserProfile(UserProfile userProfile)
        {
             _dbContext.UserProfile.Update(userProfile);

            await _dbContext.SaveChangesAsync();
        }
    }
}
