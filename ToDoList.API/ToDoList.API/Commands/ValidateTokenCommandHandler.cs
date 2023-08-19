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
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, ApiResponse>
    {

        private readonly UserManager<IdentityUser> _userManager;


        public ValidateTokenCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
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
