using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class UserLanguageController : ControllerBase
    {
        private readonly IUserLangAggregator _aggregator;
        public UserLanguageController(IUserLangAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is to get user language selection
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("selection")]
        public async Task<IActionResult> GetLanguageUser()
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                GetUserLanguageQuery userLanguageCommand = new GetUserLanguageQuery()
                {
                    UserId = userId
                };

                ApiResponse responseUserLanguage = await _aggregator.GetUserLanguageQuery(userLanguageCommand);

                if (responseUserLanguage.StatusCode == 204)
                    return StatusCode(responseUserLanguage.StatusCode);

                if (responseUserLanguage.Response == null || responseUserLanguage.Response is false)
                    return StatusCode(responseUserLanguage.StatusCode, responseUserLanguage.Response);

                return StatusCode(responseUserLanguage.StatusCode, JsonConvert.DeserializeObject<LanguageUserSelection>(responseUserLanguage.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }

        /// <summary>
        /// This method is to set user language selection
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("selection")]
        public async Task<IActionResult> SaveLanguageUser([FromBody] string userLanguage)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                SaveUserLanguageCommand userLanguageCommand = new SaveUserLanguageCommand()
                {
                    UserId = userId,
                    LanguageId = userLanguage
                };

                ApiResponse responseUserLanguage = await _aggregator.SaveUserLanguageCommand(userLanguageCommand);

                if (responseUserLanguage.Response == null || responseUserLanguage.Response is false)
                    return StatusCode(responseUserLanguage.StatusCode, responseUserLanguage.Response);

                return StatusCode(responseUserLanguage.StatusCode, JsonConvert.DeserializeObject<LanguageUserSelection>(responseUserLanguage.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }


        /// <summary>
        /// This method is to update user language selection
        /// </summary>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("selection")]
        public async Task<IActionResult> UpdateLanguageUser([FromBody] string userLanguage)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                UpdateUserLanguageCommand userLanguageCommand = new UpdateUserLanguageCommand()
                {
                    UserId = userId,
                    LanguageId = userLanguage
                };

                ApiResponse responseUserLanguage = await _aggregator.UpdateUserLanguageCommand(userLanguageCommand);

                if (responseUserLanguage.StatusCode == 204)
                    return StatusCode(responseUserLanguage.StatusCode);

                if (responseUserLanguage.Response == null || responseUserLanguage.Response is false)
                    return StatusCode(responseUserLanguage.StatusCode, responseUserLanguage.Response);

                return StatusCode(responseUserLanguage.StatusCode, JsonConvert.DeserializeObject<LanguageUserSelection>(responseUserLanguage.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }


    }
}
