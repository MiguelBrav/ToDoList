using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DTO.UsersApp;

namespace ToDoList.Domain.Interfaces
{
    public interface IUserLanguageSelectionService
    {
        Task SaveLanguageUserSelection(LanguageUserSelection userLanguage);

        Task UpdateLanguageUserSelection(LanguageUserSelection userLanguage);

        Task<LanguageUserSelection> GetLanguageUserSelection(string userId);
    }
}
