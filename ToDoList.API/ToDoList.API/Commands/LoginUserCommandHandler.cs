using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.UsersApp;

namespace ToDoList.API.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            SignInResult loginResult = await _signManager.PasswordSignInAsync(request.Email, request.Password,isPersistent: false, lockoutOnFailure: false);

            if (loginResult == null || loginResult.Succeeded == false)
            {
                response.Response = false;
                response.ResponseMessage = "The user could not login.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
            }
            
            ResponseAuth tokenLogin = await GenerateToken(request);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(tokenLogin);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }

        private async Task<ResponseAuth> GenerateToken(LoginUserCommand request)
        {


            var usuario = await _userManager.FindByEmailAsync(request.Email);

            var claimsDB = await _userManager.GetClaimsAsync(usuario);

           var claims = new List<Claim>()
            {
                new Claim("email", request.Email),
                new Claim("userId", usuario.Id)
            };

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key_jwt"]));

            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null,
                audience: null, claims: claims, expires: expiracion,
                signingCredentials: creds);

            return new ResponseAuth()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiracion
            };
        }
    }
}
