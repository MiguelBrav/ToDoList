using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Aggregators.Interfaces;
using ToDoList.API.Queries;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Controllers
{
    [Route("[controller]")]
    public class ValidateController : ControllerBase
    {
        private readonly IValidateAggregator _aggregator;
        public ValidateController(IValidateAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        /// <summary>
        /// This method is only to validate the token created.
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("validate")]
        public async Task<IActionResult> ValidateToken()
        {

            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var userClaim = HttpContext.User.Claims.Where(claim => claim.Type == "userId").FirstOrDefault();

            if(emailClaim != null && userClaim != null)
            {
                var email = emailClaim.Value;

                var userId = userClaim.Value;

                ValidateTokenQuery validateTokenQuery = new ValidateTokenQuery()
                {
                    Email = email,
                    UserId = userId
                };

                ApiResponse responseUser = await _aggregator.ValidateTokenQuery(validateTokenQuery);

                if (responseUser.Response == null || responseUser.Response is false)
                    return StatusCode(responseUser.StatusCode, responseUser.Response);

                return StatusCode(responseUser.StatusCode, responseUser.ResponseMessage);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error with token");
        }


    }
}
