using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.ApiResponse
{
    public class ResponseAuth
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
