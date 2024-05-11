using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ToDoList.DTO.ApiResponse;

namespace ToDoList.API.Commands.AdminCommands
{
    public class RemoveUserAdminCommandHandler : IRequestHandler<RemoveUserAdminCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;
        private readonly string _keyAdmin;

        public RemoveUserAdminCommandHandler(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
            _keyAdmin = _configuration.GetValue<string>("key_Admin");
        }

        public async Task<ApiResponse> Handle(RemoveUserAdminCommand request, CancellationToken cancellationToken)
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

            await _userManager.RemoveClaimAsync(user, new System.Security.Claims.Claim("IsAdmin", "1"));

            response.Response = true;
            response.ResponseMessage = "The admin role is deleted for the user";
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
