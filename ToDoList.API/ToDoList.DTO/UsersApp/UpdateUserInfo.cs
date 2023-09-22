using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.UsersApp
{
    public class UpdateUserInfo
    {
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
    }
}
