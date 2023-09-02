using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DTO.UsersApp;

namespace ToDoList.Domain.Interfaces
{
    public interface IUsersProfileService
    {
        Task SaveUserProfile(UserProfile userProfile);

        Task UpdateUserProfile(UserProfile userProfile);

        Task DeleteUserProfile(UserProfile userProfile);

        Task<UserProfile> GetUserProfile(string userId);
    }
}
