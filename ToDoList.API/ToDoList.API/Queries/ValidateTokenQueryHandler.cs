using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class ValidateTokenQueryHandler : UseCaseBase<ValidateTokenQuery, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;


        public ValidateTokenQueryHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResponse> Execute(ValidateTokenQuery request)
        {
            ApiResponse response = new ApiResponse();

            IdentityUser existsUser = await _userManager.FindByEmailAsync(request.Email);

            if (existsUser == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }
            
            if(existsUser.Id != request.UserId)
            {
                response.Response = false;
                response.ResponseMessage = "The token is not valid.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject("The token is valid.");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }     
    }
}
