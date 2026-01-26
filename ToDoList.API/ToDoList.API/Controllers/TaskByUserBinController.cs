using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands.TaskByUserBinCommands;
using ToDoList.API.Queries.TaskByUserBinQueries;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class TaskByUserBinController : ControllerBase
    {
        private readonly ITaskUserBinAggregator _aggregator;
        public TaskByUserBinController(ITaskUserBinAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is to get deleted tasks by User with filters and pagination
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("page/{pageId}/size/{sizeId}/tasktier/{taskTierId}/order/{orderById}")]
        public async Task<IActionResult> GetTaskTiersByUserBin([FromRoute] int pageId, int sizeId, int taskTierId, int orderById)
        {

            if (string.IsNullOrEmpty(pageId.ToString()) || string.IsNullOrEmpty(sizeId.ToString()) || string.IsNullOrEmpty(taskTierId.ToString()) || string.IsNullOrEmpty(orderById.ToString()))
                return StatusCode(StatusCodes.Status400BadRequest);

            if (pageId == 0 || sizeId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "The minimum value for pageId or SizeId is 1");

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                GetUserTaskBinQuery taskCommand = new GetUserTaskBinQuery()
                {
                    UserId = userId,
                    TaskTierId = taskTierId,
                    OrderById = orderById,
                    PageId = pageId,
                    SizeId = sizeId
                };

                ApiResponse responseTasks = await _aggregator.GetUserTaskBinQuery(taskCommand);

                if (responseTasks.StatusCode == 204)
                    return StatusCode(responseTasks.StatusCode);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, JsonConvert.DeserializeObject<List<TaskByUser>>(responseTasks.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to restore a list of canceled tasks from user by taskIds
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("restore/tasks")]
        public async Task<IActionResult> RestoreTasksByUser([FromBody] int[] TaskIds)
        {

            if (TaskIds.Length == 0 || TaskIds == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Task ids are not valid");
            }

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                RestoreTasksCommand taskCommand = new RestoreTasksCommand()
                {
                    UserId = userId,
                    TaskIds = TaskIds
                };

                ApiResponse responseTasks = await _aggregator.RestoreTasksCommand(taskCommand);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.Response);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to restore all canceled tasks from user 
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("restore/all/tasks")]
        public async Task<IActionResult> RestoreAllTasksByUser()
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                RestoreAllTasksCommand taskCommand = new RestoreAllTasksCommand()
                {
                    UserId = userId
                };

                ApiResponse responseTasks = await _aggregator.RestoreAllTasksCommand(taskCommand);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.Response);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to complete deleted a list of canceled tasks from user by taskIds
        /// </summary>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("clean/tasks")]
        public async Task<IActionResult> CleanBinTasksByUser([FromBody] int[] TaskIds)
        {

            if (TaskIds.Length == 0 || TaskIds == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Task ids are not valid");
            }

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                CleanTasksCommand taskCommand = new CleanTasksCommand()
                {
                    UserId = userId,
                    TaskIds = TaskIds
                };

                ApiResponse responseTasks = await _aggregator.CleanTasksCommand(taskCommand);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.Response);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to complete deleted a list of canceled tasks from user by taskIds
        /// </summary>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("clean/all/tasks")]
        public async Task<IActionResult> CleanAllBinTasksByUser()
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                CleansAllTasksCommand taskCommand = new CleansAllTasksCommand()
                {
                    UserId = userId
                };

                ApiResponse responseTasks = await _aggregator.CleansAllTasksCommand(taskCommand);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.Response);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

    }
}
