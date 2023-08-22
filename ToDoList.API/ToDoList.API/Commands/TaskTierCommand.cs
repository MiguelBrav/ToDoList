using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class TaskTierCommand : IRequest<ApiResponse>
    {
		public string LanguageId { get; set; }
	}
}
