using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.DTO
{
    public class TaskUpdateDTO
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime ExpectedDateTime { get; set; }
        public bool IsCompleted { get; set; }

    }
}
