using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Translated
{
	public class GendersTranslated
	{
		public int Id { get; set; }
		public int OriginalGenderId { get; set; }
		public string LanguageId { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }
	}
}
