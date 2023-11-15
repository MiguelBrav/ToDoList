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
    public class InstructionsTranslatedService : IInstructionsTranslatedService
    {
        private readonly AppDBContext _dbContext;

        public InstructionsTranslatedService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InstructionsTranslated>> GetInstructionsTranslateds(string languageId)
        {
            return await _dbContext.InstructionsTranslated.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<InstructionsTranslated> GetInstructionTranslatedById(int instructionId, string languageId)
        {
            return await _dbContext.InstructionsTranslated.Where(x => x.LanguageId == languageId && x.OriginalInstructionId == instructionId).FirstOrDefaultAsync();
        }
    }
}
