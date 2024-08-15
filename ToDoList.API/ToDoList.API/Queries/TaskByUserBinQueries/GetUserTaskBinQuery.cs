using MediatR;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Queries.TaskByUserBinQueries
{
    public class GetUserTasksBinCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
        public int PageId { get; set; }
        public int SizeId { get; set; }
        public int TaskTierId { get; set; }
        public int OrderById { get; set; }

    }
}
