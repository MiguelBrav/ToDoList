using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries.ReportQueries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportAggregator _aggregator;
        public ReportController(IReportAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is to get tasks by User in excel report
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("tasks/excel/{languageId}")]
        public async Task<IActionResult> GetTaskTiersExcel(string languageId)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                if (languageId == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var userId = userClaim.Value;

                GetUserTasksExcelQuery taskCommand = new GetUserTasksExcelQuery()
                {
                    UserId = userId,
                    LanguageId = languageId
                };

                ApiResponse responseTasks = await _aggregator.GetUserTasksExcelQuery(taskCommand);

                if (responseTasks.StatusCode == 204)
                    return StatusCode(responseTasks.StatusCode);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.ResponseMessage);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to get tasks in bin by User in excel report
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("tasks/excel/trash/{languageId}")]
        public async Task<IActionResult> GetTaskTiersBinExcel(string languageId)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                if (languageId == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                var userId = userClaim.Value;

                GetUserTasksBinExcelQuery taskCommand = new GetUserTasksBinExcelQuery()
                {
                    UserId = userId,
                    LanguageId = languageId
                };

                ApiResponse responseTasks = await _aggregator.GetUserTasksBinExcelQuery(taskCommand);

                if (responseTasks.StatusCode == 204)
                    return StatusCode(responseTasks.StatusCode);

                if (responseTasks.Response == null || responseTasks.Response is false)
                    return StatusCode(responseTasks.StatusCode, responseTasks.Response);

                return StatusCode(responseTasks.StatusCode, responseTasks.ResponseMessage);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }
    }
}
