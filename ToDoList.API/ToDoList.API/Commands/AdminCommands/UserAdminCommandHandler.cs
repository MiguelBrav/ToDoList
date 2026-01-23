using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Commands.AdminCommands
{
    public class UserAdminCommandHandler : UseCaseBase<UserAdminCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;
        private readonly string _keyAdmin;

        public UserAdminCommandHandler(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
            _keyAdmin = _configuration.GetValue<string>("key_Admin");
        }

        public override async Task<ApiResponse> Execute(UserAdminCommand request)
        {
            ApiResponse response = new ApiResponse();

            IdentityUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }

            if (_keyAdmin != request.KeyAdmin)
            {
                response.Response = false;
                response.ResponseMessage = "The key admin is invalid.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }

            var claimsDB = await _userManager.GetClaimsAsync(user);

            System.Security.Claims.Claim adminClaim = new System.Security.Claims.Claim("IsAdmin", "1");

            var isAdminClaim = claimsDB.FirstOrDefault(c => c.Type == "IsAdmin" && c.Value == "1");

            if (isAdminClaim != null)
            {
                response.Response = true;
                response.ResponseMessage = "The user is already admin.";
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsAdmin", "1"));

            response.Response = true;
            response.ResponseMessage = "The admin role is added for the user";
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
