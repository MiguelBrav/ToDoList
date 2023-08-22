using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Translated
{
    public class TaskTierTranslated
    {
		public int Id { get; set; }
		public int OriginalTaskTierId { get; set; }
		public string LanguageId { get; set; }
		public string TierName { get; set; }
		public string TranslatedDescription { get; set; }
		public bool IsActive { get; set; }
	}
}
