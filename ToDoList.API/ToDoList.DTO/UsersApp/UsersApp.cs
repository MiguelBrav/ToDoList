using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.UsersApp
{
    public class UsersApp
    {
		public long Id { get; set; }
		public string? UserId { get; set; }
		public DateTime BirthDate { get; set; }
		public string? Email { get; set; }
		public string? Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModificationDate { get; set; }
		public int Gender { get; set; }

	}
}
