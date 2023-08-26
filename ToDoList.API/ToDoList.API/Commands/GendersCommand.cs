using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands
{
    public class GendersCommand : IRequest<ApiResponse>
    {
		public string LanguageId { get; set; }
	}
}
