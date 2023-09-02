using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.UsersApp
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string? UserId { get; set; }
		public long UserAppId { get; set; }
		public bool IsActive { get; set; }
		public string? UrlImage { get; set; }
	}
}
