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
    public class UpdateUserAppInfoCommandHandler : IRequestHandler<UpdateUserAppInfoCommand, ApiResponse>
    {

        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        public UpdateUserAppInfoCommandHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
           
        }

        public async Task<ApiResponse> Handle(UpdateUserAppInfoCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            IdentityUser userExists = await _userManager.FindByIdAsync(request.UserId);

            if (userExists == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            UsersApp userInformation = await _usersAppService.GetUserApp(userExists.Id);

            if (userInformation == null)
            {
                response.Response = false;
                response.ResponseMessage = "The user does not exists.";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            userInformation.BirthDate = userInformation.BirthDate != request.BirthDate ? request.BirthDate : userInformation.BirthDate;
            userInformation.Name = userInformation.Name != request.Name ? request.Name : userInformation.Name;
            userInformation.Gender = userInformation.Gender != request.Gender ? request.Gender : userInformation.Gender;
            userInformation.LastModificationDate = DateTime.UtcNow;

            await _usersAppService.UpdateUserApp(userInformation);

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(userInformation);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

