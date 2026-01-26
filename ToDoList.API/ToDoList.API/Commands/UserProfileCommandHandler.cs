using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Domain.Interfaces;
using ToDoList.DTO.ApiResponse;
using ToDoList.DTO.Translated;
using ToDoList.DTO.UsersApp;
using UseCaseCore.UseCases;

namespace ToDoList.API.Commands
{
    public class UserProfileCommandHandler : UseCaseBase<UserProfileCommand, ApiResponse>
    {
        private readonly IUsersAppService _usersAppService;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUsersProfileService _usersProfileService;

        public UserProfileCommandHandler(IUsersAppService usersAppService, UserManager<IdentityUser> userManager, IUsersProfileService usersProfileService)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
            _usersProfileService = usersProfileService;
        }

        public override async Task<ApiResponse> Execute(UserProfileCommand request)
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

            UserProfile userProfile = await _usersProfileService.GetUserProfile(userExists.Id);

            if (userProfile != null)
            {
                response.Response = true;
                response.ResponseMessage = "The image already is uploaded.";
                response.StatusCode = StatusCodes.Status200OK;

                return response;
            }

            UserProfile newUserProfile = new UserProfile()
            {
                UserAppId = userInformation.Id,
                UserId = userExists.Id,
                IsActive = true,
                UrlImage = request.UrlImage
            };

            await _usersProfileService.SaveUserProfile(newUserProfile);

            response.Response = true;
            response.ResponseMessage = "The image is uploaded successfully.";
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}
