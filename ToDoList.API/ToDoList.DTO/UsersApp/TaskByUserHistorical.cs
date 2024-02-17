using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.UsersApp
{
    public class TaskByUserHistorical
    {
        public int Id { get; set; }
        public int OriginalTaskId { get; set; }
        public int OriginalTaskTierId { get; set; }
        public long UserAppId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string DeleteddUserId { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
