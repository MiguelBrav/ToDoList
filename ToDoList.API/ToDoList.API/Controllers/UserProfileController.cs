using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.API.Commands;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This method is to save user profile image
        /// </summary>
        [HttpPost]
        [Route("image")]
        public async Task<IActionResult> UserProfile([FromBody] string urlImage)
        {

            if(urlImage == null)
                return StatusCode(StatusCodes.Status400BadRequest);


            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if (userClaim != null)
            {
                var userId = userClaim.Value;

                UserProfileCommand userProfileCommand = new UserProfileCommand()
                {
                    UrlImage = urlImage,
                    UserId = userId
                };


             ApiResponse responseImage = await _mediator.Send(userProfileCommand);

             if (responseImage.Response == null || responseImage.Response is false)
                return StatusCode(responseImage.StatusCode, responseImage.Response);

             return StatusCode(responseImage.StatusCode, responseImage.ResponseMessage);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }
    }
}
