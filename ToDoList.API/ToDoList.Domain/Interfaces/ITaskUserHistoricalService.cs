﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;


namespace ToDoList.Domain.Interfaces
{
    public interface ITaskUserHistoricalService
    {
        Task<bool> SaveHistoricalUserTask(List<TaskByUserHistorical> usersTasks); 

    }
}
