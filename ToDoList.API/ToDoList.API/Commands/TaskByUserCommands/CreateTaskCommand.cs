﻿using MediatR;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.DTO;

namespace ToDoList.API.Commands.TaskByUserCommands
{
    public class CreateTaskCommand : IRequest<ApiResponse>
    {
        public TaskDTO Task { get; set; }
        public string UserId { get; set; }
    }
}
