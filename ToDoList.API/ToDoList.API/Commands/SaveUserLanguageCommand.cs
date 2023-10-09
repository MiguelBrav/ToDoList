using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class SaveUserLanguageCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
        public string LanguageId { get; set; }
    }
}
