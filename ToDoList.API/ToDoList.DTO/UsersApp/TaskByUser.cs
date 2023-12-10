using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.UsersApp
{
    public class TaskByUser
    {
        public int Id { get; set; }
        public int OriginalTaskTierId { get; set; }
        public long UserAppId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpectedDateTime { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
