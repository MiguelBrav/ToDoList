using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Commands;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersAppController : ControllerBase
    {
        private readonly IUserAppAggregator _aggregator;
        public UsersAppController(IUserAppAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is to get user information
        /// </summary>
        [HttpGet]
        [Route("userInformation")]
        public async Task<IActionResult> GetUserAppInformation()
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                GetUserAppInfoQuery userInformationCommand = new GetUserAppInfoQuery()
                {
                  UserId = userId
                };

                ApiResponse responseUserInformation = await _aggregator.GetUserAppInfoQuery(userInformationCommand);

                if (responseUserInformation.Response == null || responseUserInformation.Response is false)
                    return StatusCode(responseUserInformation.StatusCode, responseUserInformation.Response);

                return StatusCode(responseUserInformation.StatusCode, JsonConvert.DeserializeObject<UserInformation>(responseUserInformation.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }


        /// <summary>
        /// This method is to update user information
        /// </summary>
        [HttpPut]
        [Route("userInformation")]
        public async Task<IActionResult> GetUserAppInformation([FromBody] UpdateUserInfo updateUserInfo)
        {

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                UpdateUserAppInfoCommand userInformationCommand = new UpdateUserAppInfoCommand()
                {
                    UserId = userId,
                    Name = updateUserInfo.Name,
                    BirthDate = updateUserInfo.BirthDate,
                    Gender = updateUserInfo.Gender
                };

                ApiResponse responseUserInformation = await _aggregator.UpdateUserAppInfoCommand(userInformationCommand);

                if (responseUserInformation.Response == null || responseUserInformation.Response is false)
                    return StatusCode(responseUserInformation.StatusCode, responseUserInformation.Response);

                return StatusCode(responseUserInformation.StatusCode, JsonConvert.DeserializeObject<UsersApp>(responseUserInformation.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }
    }
}
