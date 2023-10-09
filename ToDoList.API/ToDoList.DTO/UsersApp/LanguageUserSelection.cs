using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.UsersApp
{
	public class LanguageUserSelection
	{
		public int Id { get; set; }
		public string LanguageId { get; set; }
		public string UserId { get; set; }
		public bool IsActive { get; set; }
	}
}
