using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Translated
{
	public class InstructionsTranslated
	{
		public int Id { get; set; }
		public int OriginalInstructionId { get; set; }
		public string LanguageId { get; set; }
		public string TranslatedName { get; set; }
		public string TranslatedDescription { get; set; }
		public string? Image { get; set; }
		public bool IsActive { get; set; }
	}
}
