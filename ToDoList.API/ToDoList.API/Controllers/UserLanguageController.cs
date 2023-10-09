using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class UserLanguageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserLanguageController(IMediator mediator)
        {
            _mediator = mediator;
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

                GetUserLanguageCommand userLanguageCommand = new GetUserLanguageCommand()
                {
                    UserId = userId
                };

                ApiResponse responseUserLanguage = await _mediator.Send(userLanguageCommand);

                if (responseUserLanguage.StatusCode == 204)
                    return StatusCode(responseUserLanguage.StatusCode);

                if (responseUserLanguage.Response == null || responseUserLanguage.Response is false)
                    return StatusCode(responseUserLanguage.StatusCode, responseUserLanguage.Response);

                return StatusCode(responseUserLanguage.StatusCode, JsonConvert.DeserializeObject<string>(responseUserLanguage.ResponseMessage));

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

                ApiResponse responseUserLanguage = await _mediator.Send(userLanguageCommand);

                if (responseUserLanguage.Response == null || responseUserLanguage.Response is false)
                    return StatusCode(responseUserLanguage.StatusCode, responseUserLanguage.Response);

                return StatusCode(responseUserLanguage.StatusCode, JsonConvert.DeserializeObject<LanguageUserSelection>(responseUserLanguage.ResponseMessage));

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }


    }
}
