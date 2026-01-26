using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using UseCaseCore.UseCases;

namespace ToDoList.API.Queries
{
    public class RefreshTokenQueryHandler : UseCaseBase<RefreshTokenQuery, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly string _keyJwt;

        public RefreshTokenQueryHandler(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _keyJwt = _configuration["key_jwt"] ?? "";
        }

        public override async Task<ApiResponse> Execute(RefreshTokenQuery request)
        {
            ApiResponse response = new ApiResponse();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }

            IList<Claim> claimsDB = await _userManager.GetClaimsAsync(user);

            ResponseAuth refreshToken = await _tokenService.GenerateToken(user, request.Email, claimsDB, _keyJwt);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(refreshToken);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }

    }
}
