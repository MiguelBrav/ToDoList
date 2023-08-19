using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.ApiResponse
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public object? Response { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
