﻿using MediatR;
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
        private readonly ITokenService _tokenService;
        private readonly string _keyJwt;
        public LoginUserCommandHandler(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _keyJwt = _configuration["key_jwt"] ?? "";
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

            var user = await _userManager.FindByEmailAsync(request.Email);

            IList<Claim> claimsDB = await _userManager.GetClaimsAsync(user);

            ResponseAuth tokenLogin = await _tokenService.GenerateToken(user, request.Email, claimsDB, _keyJwt);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(tokenLogin);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }

    }
}
